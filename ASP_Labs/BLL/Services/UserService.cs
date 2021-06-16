using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;
using WebApp.DAL.EF;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> TryRegister(UserDTO userDTO)
        {

            var user = new ApplicationUser
            {
                Email = userDTO.Email,
                UserName = userDTO.Email
            };

            var tryRegister = await _userManager.CreateAsync(user, userDTO.Password);

            if (tryRegister.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
                return codeEncoded;
            }

            return null;
        }

        public async Task<bool> TryLogin(UserDTO userDTO)
        {
            var tryLogin = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, isPersistent: false, false);

            return tryLogin.Succeeded;
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null && token != null)
                {
                    var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
                    var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
                    var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);
                    if (result.Succeeded)
                        return true;
                }
            }

            return false;
        }

        public async Task<ApplicationUser> UserWithoutPassword(ApplicationUser user)
        {
            using (var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().
                UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options))
            {
                context.Entry(user).State = EntityState.Modified;
            }
                return user;
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
    }
}
