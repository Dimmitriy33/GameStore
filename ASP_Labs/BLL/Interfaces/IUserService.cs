using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<string> TryRegister(UserDTO userDTO);
        Task<bool> TryLogin(UserDTO userDTO);
        Task<bool> ConfirmEmail(string email, string token);
        Task<ApplicationUser> UserWithoutPassword(ApplicationUser user);
        Task<bool> ChangePassword(string email, string newPassword);
    }
}
