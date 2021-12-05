using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
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
            var userForUpdate = GetUser(user.Id);
            userForUpdate.UserName = user.UserName;
            userForUpdate.AddressDelivery = user.AddressDelivery;
            userForUpdate.PhoneNumber = user.PhoneNumber;

            _dbContext.Users.Update(userForUpdate);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePasswordAsync(Guid id, string oldPassword, string newPassword)
        {
            var userForUpdate = GetUser(id);

            var result = await _userManager.ChangePasswordAsync(userForUpdate, oldPassword, newPassword);

            if (result.Succeeded)
            {
                _dbContext.Users.Update(userForUpdate);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return _mapper.Map<UserDTO>(user);
        }

        private ApplicationUser GetUser(Guid id) => _dbContext.Users.FirstOrDefault(u => u.Id == id);
    }
}
