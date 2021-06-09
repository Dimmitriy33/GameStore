using Microsoft.AspNetCore.Identity;
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

        public async Task<bool> TryRegister(UserDTO userDTO)
        {

            var user = new IdentityUser
            {
                Email = userDTO.Email,
                UserName = userDTO.Email
            };

            var tryRegister = await _userManager.CreateAsync(user, userDTO.Password);

            if (tryRegister.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }

            return false;
        }

        public async Task<bool> TryLogin(UserDTO userDTO)
        {
            var tryLogin = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, isPersistent: false, false);

            return tryLogin.Succeeded;
        }
    }
}
