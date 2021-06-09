using System.Threading.Tasks;
using WebApp.BLL.DTO;

namespace WebApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<bool> TryRegister(UserDTO userDTO);
        Task<bool> TryLogin(UserDTO userDTO);
    }
}
