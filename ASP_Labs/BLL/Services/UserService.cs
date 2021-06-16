using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Helpers;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;

namespace WebApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<ServiceResult<string>> TryRegister(UserDTO userDTO)
        {

            var user = new IdentityUser
            {
                Email = userDTO.Email,
                UserName = userDTO.Email
            };

            var tryRegister = await _userManager.CreateAsync(user, userDTO.Password);

            if (!tryRegister.Succeeded)
            {
                return new ServiceResult<string> { Result = "Invalid Register Attempt", ServiceResultType = ServiceResultType.Error };
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                return new ServiceResult<string> { Result = "Missing role", ServiceResultType = ServiceResultType.Error };
            }

            await _userManager.AddToRoleAsync(user, "User");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var codeEncoded = TokenEncodingHelper.Encode(token);

            return new ServiceResult<string> { Result = codeEncoded, ServiceResultType = ServiceResultType.Success };
        }

        public async Task<bool> TryLogin(UserDTO userDTO)
        {
            var tryLogin = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, isPersistent: false, false);

            return tryLogin.Succeeded;
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            if (email == null || token == null)
            {
                return false;
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var codeDecoded = TokenEncodingHelper.Decode(token);
            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
