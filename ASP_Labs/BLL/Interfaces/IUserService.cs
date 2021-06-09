using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApp.BLL.DTO;

namespace WebApp.BLL.Interfaces
{
    public interface IUserService
    {
        public Task<bool> TryRegister(UserDTO userDTO, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager);
        public Task<bool> TryLogin(UserDTO userDTO, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager);
    }
}
