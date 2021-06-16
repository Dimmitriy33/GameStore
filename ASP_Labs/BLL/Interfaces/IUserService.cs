using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<string>> TryRegister(UserDTO userDTO);
        public Task<bool> TryLogin(UserDTO userDTO);
        Task<bool> ConfirmEmail(string email, string token);
        public Task<ServiceResult<ApplicationUser>> UpdateUser(ApplicationUser user);
        public Task<ServiceResult<ApplicationUser>> FindUser(ApplicationUser user);
        Task<bool> ChangePassword(string email, string newPassword);
    }
}
