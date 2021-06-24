using System;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.DAL.Entities;

namespace WebApp.DAL.Interfaces.Database
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<UserDTO> GetUserByIdAsync(Guid Id);
        Task UpdateUserInfoAsync(UserDTO user);
        Task UpdatePasswordAsync(Guid id, string oldPassword, string newPassword);
    }
}
