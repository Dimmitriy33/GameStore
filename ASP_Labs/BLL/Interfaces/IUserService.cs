using System;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Models;

namespace WebApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<string>> TryRegisterAsync(SignUpUserDTO userDTO);
        Task<ServiceResult> ConfirmEmailAsync(string email, string token);
        Task<ServiceResult<string>> TryLoginAsync(SignInUserDTO userDTO);
        Task<ServiceResult> ChangePasswordAsync(ResetPasswordUserDTO use);
        Task<ServiceResult<UserDTO>> FindUserByIdAsync(Guid id);
        Task<ServiceResult<UserDTO>> UpdateUserInfoAsync(UserDTO user);
    }
}
