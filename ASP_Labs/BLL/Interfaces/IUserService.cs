﻿using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResultClass<string>> TryRegister(UserDTO userDTO);
        Task<bool> TryLogin(UserDTO userDTO);
        Task<ServiceResultStruct<bool>> ConfirmEmail(string email, string token);
        Task<ServiceResultStruct<bool>> ChangePassword(ApplicationUser Appuser, string newPassword);
        Task<ServiceResultClass<ApplicationUser>> UpdateUser(ApplicationUser user);
        Task<ServiceResultClass<ApplicationUser>> FindUser(ApplicationUser user);
    }
}
