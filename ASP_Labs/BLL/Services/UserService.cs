using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;

namespace WebApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> TryRegister(UserDTO userDTO)
        {

            var user = new IdentityUser
            {
                Email = userDTO.Email,
                UserName = userDTO.Email
            };

            var tryRegister = await _userManager.CreateAsync(user, userDTO.Password);

            if (tryRegister.Succeeded)
            {
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
    }
}
