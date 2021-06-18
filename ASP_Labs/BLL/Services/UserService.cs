using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.BLL.Helpers;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;
using WebApp.DAL.EF;
using WebApp.Web.Startup.Settings;

namespace WebApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtGenerator _jwtGenerator;

        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IJwtGenerator jwtGenerator,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration, AppSettings appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtGenerator = jwtGenerator;
			_configuration = configuration;
			_appSettings = appSettings;	
        }
		
		public async Task<ServiceResultClass<string>> TryRegister(UserDTO userDTO)
        {
            var user = new ApplicationUser
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

        public async Task<ServiceResultClass<ApplicationUser>> UpdateUser(ApplicationUser user)
        {
            if(user == null)
            {
                return new ServiceResultClass<ApplicationUser> { Result = user, ServiceResultType = ServiceResultType.Error };
            }

            using (var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().
                UseSqlServer(_configuration.GetConnectionString(_appSettings.DbSettings.ConnectionString)).Options))
            {
                var userForUpdate = await _userManager.FindByIdAsync(user.Id);

                if (userForUpdate == null)
                {
                    return new ServiceResultClass<ApplicationUser> { Result = user, ServiceResultType = ServiceResultType.Error };
                }

                context.Entry(userForUpdate.PasswordHash).State = EntityState.Detached;
                userForUpdate = user;
                context.SaveChanges();
                return new ServiceResultClass<ApplicationUser> { Result = userForUpdate, ServiceResultType = ServiceResultType.Success };
            }
        }

        public async Task<ServiceResultStruct<bool>> ChangePassword(ApplicationUser Appuser, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(Appuser.Id);

            if (user == null)
            {
                return new ServiceResultStruct<bool> { Result = false, ServiceResultType = ServiceResultType.Error, Message = "Can't find this user" };
            }

            if (!(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return new ServiceResultStruct<bool> { Result = false, ServiceResultType = ServiceResultType.Error, Message = "Email is not confirmed" };
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                return new ServiceResultStruct<bool> { Result = true, ServiceResultType = ServiceResultType.Success, Message = "Password changed" };
            }

            return new ServiceResultStruct<bool> { Result = false, ServiceResultType = ServiceResultType.Error, Message = "Password is not changed" };

        }

        public async Task<ServiceResultClass<ApplicationUser>> FindUser(ApplicationUser user)
        {
            var findedUser = await _userManager.FindByIdAsync(user.Id);

            if(findedUser == null)
            {
                return new ServiceResultClass<ApplicationUser> { Result = user, ServiceResultType = ServiceResultType.Error };
            }

            return new ServiceResultClass<ApplicationUser> { Result = findedUser, ServiceResultType = ServiceResultType.Success };
        }
    }
}
