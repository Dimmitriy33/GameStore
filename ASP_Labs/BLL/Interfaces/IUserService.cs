using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Models;

namespace WebApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<string>> TryRegister(UserDTO userDTO);
        Task<bool> TryLogin(UserDTO userDTO);
        Task<bool> ConfirmEmail(string email, string token);
    }
}
