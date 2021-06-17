using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApp.BLL.Constants;
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

        public async Task<ServiceResultClass<string>> TryRegister(UserDTO userDTO)
        {

            var user = new IdentityUser
            {
                Email = userDTO.Email,
                UserName = userDTO.Email
            };

            var tryRegister = await _userManager.CreateAsync(user, userDTO.Password);

            if (!tryRegister.Succeeded)
            {
                return new ServiceResultClass<string> { Result = "Invalid Register Attempt", ServiceResultType = ServiceResultType.Error };
            }

            if (!await _roleManager.RoleExistsAsync(RolesConstants.User))
            {
                return new ServiceResultClass<string> { Result = "Missing role", ServiceResultType = ServiceResultType.Error };
            }

            await _userManager.AddToRoleAsync(user, RolesConstants.User);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var codeEncoded = TokenEncodingHelper.Encode(token);

            return new ServiceResultClass<string> { Result = codeEncoded, ServiceResultType = ServiceResultType.Success };
        }

        public async Task<bool> TryLogin(UserDTO userDTO)
        {
            var tryLogin = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, isPersistent: false, false);

            return tryLogin.Succeeded;
        }

        public async Task<ServiceResultStruct<bool>> ConfirmEmail(string email, string token)
        {
            if (email == null)
            {
                return new ServiceResultStruct<bool> { Result = false, ServiceResultType = ServiceResultType.Error, Message="Invalid email" };
            }

            if (token == null)
            {
                return new ServiceResultStruct<bool> { Result = false, ServiceResultType = ServiceResultType.Error, Message = "Invalid token" };
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ServiceResultStruct<bool> { Result = false, ServiceResultType = ServiceResultType.Error, Message = "Can't find this email" };
            }

            var codeDecoded = TokenEncodingHelper.Decode(token);
            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            if (result.Succeeded)
            {
                return new ServiceResultStruct<bool> { Result = true, ServiceResultType = ServiceResultType.Success, Message = "Email address confirmed" };
            }

            return new ServiceResultStruct<bool> { Result = false, ServiceResultType = ServiceResultType.Error, Message = "Can't confirm email" };
        }
    }
}
