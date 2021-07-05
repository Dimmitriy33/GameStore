using System;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Models;

namespace WebApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResultClass<string>> TryRegisterAsync(SignUpUserDTO userDTO);
        Task<ServiceResult> ConfirmEmailAsync(string email, string token);
        Task<ServiceResultClass<string>> TryLoginAsync(SignInUserDTO userDTO);
        Task<ServiceResult> ChangePasswordAsync(ResetPasswordUserDTO use);
        Task<ServiceResultClass<UserDTO>> FindUserByIdAsync(Guid id);
        Task<ServiceResultClass<UserDTO>> UpdateUserInfoAsync(UserDTO user);
    }
}
