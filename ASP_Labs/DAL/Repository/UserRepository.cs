using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.DAL.EF;
using WebApp.DAL.Entities;
using WebApp.DAL.Interfaces.Database;

namespace WebApp.DAL.Repository
{
    public class UserRepository : Repository<ApplicationDbContext, ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IMapper mapper) : base(dbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task UpdateUserInfoAsync(UserDTO user)
        {
            var userForUpdate = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
            userForUpdate.UserName = user.UserName;
            userForUpdate.AddressDelivery = user.AddressDelivery;
            await _userManager.SetPhoneNumberAsync(userForUpdate, user.PhoneNumber);
            userForUpdate.PhoneNumber = user.PhoneNumber;

            _dbContext.Users.Update(userForUpdate);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePasswordAsync(string id, string oldPassword, string newPassword)
        {
            var userForUpdate = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            var result = await _userManager.ChangePasswordAsync(userForUpdate, oldPassword, newPassword);

            if (result.Succeeded)
            {
                _dbContext.Users.Update(userForUpdate);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<UserDTO> GetUserByIdAsync(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            return _mapper.Map<UserDTO>(user);
        }
    }
}
