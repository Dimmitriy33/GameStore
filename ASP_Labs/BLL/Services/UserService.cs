using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.BLL.Helpers;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtGenerator _jwtGenerator;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<ServiceResultClass<string>> TryRegister(UserDTO userDTO)
        {
            var user = new ApplicationUser
            {
                Email = userDTO.Email,
                UserName = userDTO.Email,
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

        public async Task<ServiceResultClass<string>> TryLogin(UserDTO userDTO)
        {

            var tryLogin = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, isPersistent: false, false);

            if (tryLogin.Succeeded)
            {
                var user = _userManager.FindByEmailAsync(userDTO.Email);
                var jwtToken = _jwtGenerator.CreateToken((ApplicationUser)user.Result);
                return new ServiceResultClass<string> { Result = jwtToken, ServiceResultType = ServiceResultType.Success };
            }

            return new ServiceResultClass<string> { Result = "Invaild Login Attempt", ServiceResultType = ServiceResultType.Error };
        }

        public async Task<ServiceResult> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ServiceResult { ServiceResultType = ServiceResultType.Error, Message = "Can't find this email" };
            }

            var codeDecoded = TokenEncodingHelper.Decode(token);
            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            if (result.Succeeded)
            {
                return new ServiceResult { ServiceResultType = ServiceResultType.Success, Message = "Email address confirmed" };
            }

            return new ServiceResult { ServiceResultType = ServiceResultType.Error, Message = "Can't confirm email" };
        }
    }
}