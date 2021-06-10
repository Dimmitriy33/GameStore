using System.Threading.Tasks;
using WebApp.BLL.DTO;

namespace WebApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<string> TryRegister(UserDTO userDTO);
        Task<bool> TryLogin(UserDTO userDTO);
        Task<bool> ConfirmEmail(string email, string token);
    }
}
