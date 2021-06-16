using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Helpers;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.EF;
using WebApp.DAL.Entities;
using WebApp.Web.Startup.Settings;

namespace WebApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration, AppSettings appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _appSettings = appSettings;
        }

        public async Task<ServiceResult<string>> TryRegister(UserDTO userDTO)
        {

            var user = new ApplicationUser
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

        public async Task<ServiceResult<ApplicationUser>> UpdateUser(ApplicationUser user)
        {
            if(user == null)
            {
                return new ServiceResult<ApplicationUser> { Result = user, ServiceResultType = ServiceResultType.Error };
            }

            using (var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().
                UseSqlServer(_configuration.GetConnectionString(_appSettings.DbSettings.ConnectionString)).Options))
            {
                var userForUpdate = await _userManager.FindByEmailAsync(user.Email);

                if (userForUpdate == null)
                {
                    return new ServiceResult<ApplicationUser> { Result = user, ServiceResultType = ServiceResultType.Error };
                }

                context.Entry(userForUpdate.PasswordHash).State = EntityState.Detached;
                userForUpdate = user;
                context.SaveChanges();
                return new ServiceResult<ApplicationUser> { Result = userForUpdate, ServiceResultType = ServiceResultType.Success };
            }
        }

        public async Task<bool> ChangePassword(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (email == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return false;
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
                return true;

            return false;

        }

        public async Task<ServiceResult<ApplicationUser>> FindUser(ApplicationUser user)
        {
            var findedUser = await _userManager.FindByEmailAsync(user.Email);

            if(findedUser == null)
                return new ServiceResult<ApplicationUser> { Result = user, ServiceResultType = ServiceResultType.Error };

            return new ServiceResult<ApplicationUser> { Result = findedUser, ServiceResultType = ServiceResultType.Success };
        }
    }
}
