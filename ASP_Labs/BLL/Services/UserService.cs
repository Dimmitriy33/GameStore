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

        #endregion

        #region Services

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ITokenEncodingHelper _tokenEncodingHelper;
        private readonly IMapper _mapper;

        #endregion

        #region Repositories

        private readonly IUserRepository _userRepository;

        #endregion

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtGenerator jwtGenerator,
            RoleManager<ApplicationRole> roleManager, IUserRepository userRepository, IMapper mapper, ITokenEncodingHelper tokenEncodingHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtGenerator = jwtGenerator;
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenEncodingHelper = tokenEncodingHelper;
        }

        public async Task<ServiceResultClass<string>> TryRegisterAsync(AuthUserDTO userDTO)
        {
            var user = new ApplicationUser
            {
                Email = userDTO.Email,
                UserName = userDTO.Email
            };

            var tryRegister = await _userManager.CreateAsync(user, userDTO.Password);

            if (!tryRegister.Succeeded)
            {
                return new ServiceResultClass<string>(InvalidRegisterMessage, ServiceResultType.Bad_Request);
            }

            if (!await _roleManager.RoleExistsAsync(RolesConstants.User))
            {
                return new ServiceResultClass<string>(MissingRole, ServiceResultType.Bad_Request);
            }

            await _userManager.AddToRoleAsync(user, RolesConstants.User);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var codeEncoded = _tokenEncodingHelper.Encode(token);

            return new ServiceResultClass<string>(codeEncoded, ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<string>> TryLoginAsync(AuthUserDTO userDTO)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            if (user is null)
            {
                return new ServiceResultClass<string>(InvalidLoginMessage, ServiceResultType.Bad_Request);
            }

            var tryLogin = await _signInManager.CheckPasswordSignInAsync(user, userDTO.Password, false);

            if (tryLogin.Succeeded)
            {
                var jwtToken = _jwtGenerator.CreateToken(user.Id, user.UserName, _userManager.GetRolesAsync(user).Result[0]);
                return new ServiceResultClass<string>(jwtToken, ServiceResultType.Success);
            }

            return new ServiceResultClass<string>(InvalidLoginMessage, ServiceResultType.Unauthorized);
        }

        public async Task<ServiceResult> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return new ServiceResult(NotFoundEmail, ServiceResultType.Invalid_Data);
            }

            var codeDecoded = _tokenEncodingHelper.Decode(token);
            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            if (result.Succeeded)
            {
                return new ServiceResult(ServiceResultType.Success);
            }

            return new ServiceResult(NotConfirmedEmail, ServiceResultType.Bad_Request);
        }

        public async Task<ServiceResultClass<UserDTO>> UpdateUserInfoAsync(UserDTO user)
        {
            await _userRepository.UpdateUserInfoAsync(user);
            var updatedUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if(updatedUser is null)
            {
                return new ServiceResultClass<UserDTO>(ServiceResultType.Bad_Request);
            }

            return new ServiceResultClass<UserDTO>(_mapper.Map<UserDTO>(updatedUser), ServiceResultType.Success);
        }

        public async Task<ServiceResult> ChangePasswordAsync(ResetPasswordUserDTO user)
        {
            var userForUpdate = await _userManager.FindByIdAsync(user.Id.ToString());

            if (userForUpdate is null)
            {
                return new ServiceResult(NotFoundUser, ServiceResultType.Bad_Request);
            }

            await _userRepository.UpdatePasswordAsync(user.Id, user.OldPassword, user.NewPassword);

            return new ServiceResult(ServiceResultType.Success);

        }

        public async Task<ServiceResultClass<UserDTO>> FindUserByIdAsync(Guid id)
        {
            var foundUser = await _userRepository.GetUserByIdAsync(id);

            if (foundUser is null)
            {
                return new ServiceResultClass<UserDTO>(ServiceResultType.Not_Found);
            }

            return new ServiceResultClass<UserDTO>(foundUser, ServiceResultType.Success );
        }
    }
}
