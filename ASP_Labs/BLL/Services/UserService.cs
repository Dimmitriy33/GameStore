using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;
using WebApp.DAL.Interfaces.Database;
using WebApp.DAL.Interfaces.Redis;

namespace WebApp.BLL.Services
{
    public class UserService : IUserService
    {
        #region Constants

        private const string InvalidRegisterMessage = "Invalid Register Attempt";
        private const string InvalidLoginMessage = "Invalid Login Attempt";
        private const string MissingRole = "Missing role";
        private const string NotFoundEmail = "Email not found";
        private const string NotConfirmedEmail = "Email not confirmed";
        private const string NotFoundUser = "User not found";
        private const int DefaultRedisCacheExpireSec = 1200;

        #endregion

        #region Services

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ITokenEncodingHelper _tokenEncodingHelper;
        private readonly IMapper _mapper;

        #endregion

        #region DAL

        private readonly IRedisContext _redisContext;
        private readonly IUserRepository _userRepository;

        #endregion

        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtGenerator jwtGenerator,
            IUserRepository userRepository,
            IMapper mapper,
            ITokenEncodingHelper tokenEncodingHelper,
            IRedisContext redisContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtGenerator = jwtGenerator;
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenEncodingHelper = tokenEncodingHelper;
            _redisContext = redisContext;
        }

        public async Task<ServiceResultClass<string>> TryRegisterAsync(SignUpUserDTO userDTO)
        {
            var user = new ApplicationUser
            {
                Email = userDTO.Email,
                UserName = userDTO.UserName,
                AddressDelivery = userDTO.AddressDelivery,
                PhoneNumber = userDTO.PhoneNumber
            };

            var tryRegister = await _userManager.CreateAsync(user, userDTO.Password);

            if (!tryRegister.Succeeded)
            {
                return new ServiceResultClass<string>(InvalidRegisterMessage, ServiceResultType.BadRequest);
            }

            if (!await _roleManager.RoleExistsAsync(RolesConstants.User))
            {
                return new ServiceResultClass<string>(MissingRole, ServiceResultType.BadRequest);
            }

            await _userManager.AddToRoleAsync(user, RolesConstants.User);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var codeEncoded = _tokenEncodingHelper.Encode(token);

            return new ServiceResultClass<string>(result: codeEncoded, ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<string>> TryLoginAsync(SignInUserDTO userDTO)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            if (user is null)
            {
                return new ServiceResultClass<string>(InvalidLoginMessage, ServiceResultType.BadRequest);
            }

            var tryLogin = await _signInManager.CheckPasswordSignInAsync(user, userDTO.Password, false);

            if (tryLogin.Succeeded)
            {
                var jwtToken = _jwtGenerator.CreateToken(user.Id, user.UserName, _userManager.GetRolesAsync(user).Result[0]);

                await _redisContext.Set(CreateRedisKeyForUser(user.Id), user, TimeSpan.FromSeconds(DefaultRedisCacheExpireSec));

                return new ServiceResultClass<string>(result: jwtToken, ServiceResultType.Success);
            }

            return new ServiceResultClass<string>(InvalidLoginMessage, ServiceResultType.Unauthorized);
        }

        public async Task<ServiceResult> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return new ServiceResult(NotFoundEmail, ServiceResultType.InvalidData);
            }

            var codeDecoded = _tokenEncodingHelper.Decode(token);
            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            if (result.Succeeded)
            {
                return new ServiceResult(ServiceResultType.Success);
            }

            return new ServiceResult(NotConfirmedEmail, ServiceResultType.BadRequest);
        }

        public async Task<ServiceResultClass<UserDTO>> UpdateUserInfoAsync(UserDTO user)
        {
            await _userRepository.UpdateUserInfoAsync(user);
            var updatedUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if (updatedUser is null)
            {
                return new ServiceResultClass<UserDTO>(ServiceResultType.BadRequest);
            }

            await _redisContext.Remove<ApplicationUser>(CreateRedisKeyForUser(user.Id));

            return new ServiceResultClass<UserDTO>(_mapper.Map<UserDTO>(updatedUser), ServiceResultType.Success);
        }

        public async Task<ServiceResult> ChangePasswordAsync(ResetPasswordUserDTO user)
        {
            var userForUpdate = await _redisContext.Get<ApplicationUser>(CreateRedisKeyForUser(user.Id));

            if (userForUpdate is null)
            {
                userForUpdate = await _userManager.FindByIdAsync(user.Id.ToString());
            }

            if (userForUpdate is null)
            {
                return new ServiceResult(NotFoundUser, ServiceResultType.BadRequest);
            }

            await _userRepository.UpdatePasswordAsync(user.Id, user.OldPassword, user.NewPassword);

            return new ServiceResult(ServiceResultType.Success);

        }

        public async Task<ServiceResultClass<UserDTO>> FindUserByIdAsync(Guid id)
        {
            var foundUserByRedis = await _redisContext.Get<ApplicationUser>(CreateRedisKeyForUser(id));

            if (foundUserByRedis is not null)
            {
                return new ServiceResultClass<UserDTO>(_mapper.Map<UserDTO>(foundUserByRedis), ServiceResultType.Success);
            }

            var foundUser = await _userRepository.GetUserByIdAsync(id);

            if (foundUser is null)
            {
                return new ServiceResultClass<UserDTO>(ServiceResultType.NotFound);
            }

            return new ServiceResultClass<UserDTO>(foundUser, ServiceResultType.Success);
        }

        private static string CreateRedisKeyForUser(Guid userId) => $"u_{userId}";
    }
}
