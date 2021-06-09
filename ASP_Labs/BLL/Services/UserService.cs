using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;

namespace WebApp.BLL.Services
{
    public class UserService : IUserService
    {
        public async Task<bool> TryRegister(UserDTO userDTO, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {

            var user = new IdentityUser
            {
                Email = userDTO.Email,
            };

            var tryRegister = await userManager.CreateAsync(user, userDTO.Password);

            if (tryRegister.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }

            return false;
        }

        public async Task<bool> TryLogin(UserDTO userDTO, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            var tryLogin = await signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, isPersistent: false, false);

            return tryLogin.Succeeded;
        }
    }
}
