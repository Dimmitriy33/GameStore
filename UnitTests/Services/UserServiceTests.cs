using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using UnitTests.Constants;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.BLL.Helpers;
using WebApp.BLL.Mappers;
using WebApp.BLL.Models;
using WebApp.BLL.Services;
using WebApp.DAL.Entities;
using WebApp.DAL.Interfaces.Database;
using WebApp.DAL.Interfaces.Redis;
using WebApp.Web.Startup.Settings;
using Xunit;

namespace UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly AppSettings appSettings = new AppSettings
        {
            JwtSettings = new JwtSettings
            {
                Audience = "webApp-audience",
                Issuer = "webApp",
                TokenKey = "TesTKeykYrlik123",
                Lifetime = 60,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true
            }
        };

        [Fact]
        public async Task ShouldRegister_ReturnServiceResultClassWithToken()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var token = TestValues.TestToken;
            var registerResult = IdentityResult.Success;
            var roleExistenceResult = true;


            var signUpUser = new SignUpUserDTO
            {
                AddressDelivery = user.AddressDelivery,
                Email = user.Email,
                Password = TestValues.TestPassword1,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };

            A.CallTo(() => userManager.CreateAsync(A<ApplicationUser>._, signUpUser.Password)).Returns(registerResult);
            A.CallTo(() => roleManager.RoleExistsAsync(RolesConstants.User)).Returns(roleExistenceResult);
            A.CallTo(() => userManager.GenerateEmailConfirmationTokenAsync(A<ApplicationUser>._)).Returns(token);
            var tokenEncoded = tokenEncodingHelper.Encode(token);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.TryRegisterAsync(signUpUser);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.Equal(tokenEncoded, result.Result);

            A.CallTo(() => userManager.AddToRoleAsync(A<ApplicationUser>._, RolesConstants.User)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ShouldNotRegister_ReturnServiceResultClassWithErrorMessage()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var registerResult = IdentityResult.Failed();


            var signUpUser = new SignUpUserDTO
            {
                AddressDelivery = user.AddressDelivery,
                Email = user.Email,
                Password = TestValues.TestPassword1,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };

            A.CallTo(() => userManager.CreateAsync(A<ApplicationUser>._, signUpUser.Password)).Returns(registerResult);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.TryRegisterAsync(signUpUser);

            //Assert
            Assert.Equal(ServiceResultType.Bad_Request, result.ServiceResultType);

            A.CallTo(() => userManager.GenerateEmailConfirmationTokenAsync(A<ApplicationUser>._)).MustNotHaveHappened();
            A.CallTo(() => roleManager.RoleExistsAsync(RolesConstants.User)).MustNotHaveHappened();
            A.CallTo(() => userManager.AddToRoleAsync(A<ApplicationUser>._, RolesConstants.User)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ShouldNotRegisterWithoutRoles_ReturnServiceResultClassWithErrorMessage()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var registerResult = IdentityResult.Success;
            var roleExistenceResult = false;


            var signUpUser = new SignUpUserDTO
            {
                AddressDelivery = user.AddressDelivery,
                Email = user.Email,
                Password = TestValues.TestPassword1,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };

            A.CallTo(() => userManager.CreateAsync(A<ApplicationUser>._, signUpUser.Password)).Returns(registerResult);
            A.CallTo(() => roleManager.RoleExistsAsync(RolesConstants.User)).Returns(roleExistenceResult);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.TryRegisterAsync(signUpUser);

            //Assert
            Assert.Equal(ServiceResultType.Bad_Request, result.ServiceResultType);

            A.CallTo(() => userManager.GenerateEmailConfirmationTokenAsync(A<ApplicationUser>._)).MustNotHaveHappened();
            A.CallTo(() => roleManager.RoleExistsAsync(RolesConstants.User)).MustHaveHappenedOnceExactly();
            A.CallTo(() => userManager.AddToRoleAsync(A<ApplicationUser>._, RolesConstants.User)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ShouldLogin_ReturnServiceResultClassWithToken()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var loginResult = SignInResult.Success;


            var signInUser = new SignInUserDTO
            {
                Email = user.Email,
                Password = TestValues.TestPassword1
            };

            A.CallTo(() => userManager.FindByEmailAsync(signInUser.Email)).Returns(user);
            A.CallTo(() => signInManager.CheckPasswordSignInAsync(A<ApplicationUser>._, signInUser.Password, false)).Returns(loginResult);
            var jwtToken = jwtGenerator.CreateToken(user.Id, user.UserName, RolesConstants.User);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.TryLoginAsync(signInUser);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.NotNull(result.Result);

            A.CallTo(() => redisContext.Set(A<string>._, A<ApplicationUser>._, A<TimeSpan>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ShouldNotLoginInvalidPassword_ReturnServiceResultClassWithErrorMessage()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var loginResult = SignInResult.Success;


            var signInUser = new SignInUserDTO
            {
                Email = user.Email,
                Password = TestValues.TestPassword1
            };

            A.CallTo(() => userManager.FindByEmailAsync(signInUser.Email)).Returns((ApplicationUser)null);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.TryLoginAsync(signInUser);

            //Assert
            Assert.Equal(ServiceResultType.Bad_Request, result.ServiceResultType);
            Assert.Null(result.Result);

            A.CallTo(() => signInManager.CheckPasswordSignInAsync(A<ApplicationUser>._, signInUser.Password, false)).MustNotHaveHappened();
            A.CallTo(() => redisContext.Set(A<string>._, A<ApplicationUser>._, A<TimeSpan>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ShouldNotLoginNotFoundUser_ReturnServiceResultClassWithErrorMessage()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var loginResult = SignInResult.Failed;


            var signInUser = new SignInUserDTO
            {
                Email = user.Email,
                Password = TestValues.TestPassword1
            };

            A.CallTo(() => userManager.FindByEmailAsync(signInUser.Email)).Returns(user);
            A.CallTo(() => signInManager.CheckPasswordSignInAsync(A<ApplicationUser>._, signInUser.Password, false)).Returns(loginResult);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.TryLoginAsync(signInUser);

            //Assert
            Assert.Equal(ServiceResultType.Unauthorized, result.ServiceResultType);
            Assert.Null(result.Result);

            A.CallTo(() => signInManager.CheckPasswordSignInAsync(A<ApplicationUser>._, signInUser.Password, false)).MustHaveHappenedOnceExactly();
            A.CallTo(() => redisContext.Set(A<string>._, A<ApplicationUser>._, A<TimeSpan>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ShouldConfirmEmail_ReturnServiceResult()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var token = TestValues.TestToken;
            var confirmEmailResult = IdentityResult.Success;

            var decodedToken = tokenEncodingHelper.Decode(token);

            A.CallTo(() => userManager.FindByEmailAsync(user.Email)).Returns(user);
            A.CallTo(() => userManager.ConfirmEmailAsync(A<ApplicationUser>._, decodedToken)).Returns(confirmEmailResult);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.ConfirmEmailAsync(user.Email, token);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
        }

        [Fact]
        public async Task ShouldNotConfirmEmailNotFoundUser_ReturnServiceResult()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var token = TestValues.TestToken;

            A.CallTo(() => userManager.FindByEmailAsync(user.Email)).Returns((ApplicationUser)null);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.ConfirmEmailAsync(user.Email, token);

            //Assert
            Assert.Equal(ServiceResultType.Invalid_Data, result.ServiceResultType);

            A.CallTo(() => userManager.ConfirmEmailAsync(A<ApplicationUser>._, A<string>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ShouldNotConfirmEmailInvalidToken_ReturnServiceResult()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var token = TestValues.TestToken;
            var confirmEmailResult = IdentityResult.Failed();

            var decodedToken = tokenEncodingHelper.Decode(token);

            A.CallTo(() => userManager.FindByEmailAsync(user.Email)).Returns(user);
            A.CallTo(() => userManager.ConfirmEmailAsync(A<ApplicationUser>._, decodedToken)).Returns(confirmEmailResult);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.ConfirmEmailAsync(user.Email, token);

            //Assert
            Assert.Equal(ServiceResultType.Bad_Request, result.ServiceResultType);

            A.CallTo(() => userManager.ConfirmEmailAsync(A<ApplicationUser>._, A<string>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ShouldUpdateUserInfo_ReturnServiceResultClassWithUserDTO()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var userDTO = mapper.Map<UserDTO>(user);

            A.CallTo(() => userRepository.UpdateUserInfoAsync(A<UserDTO>._)).DoesNothing();
            A.CallTo(() => userManager.FindByIdAsync(user.Id.ToString())).Returns(user);
            A.CallTo(() => redisContext.Remove<ApplicationUser>(A<string>._, A<TimeSpan>._)).DoesNothing();

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.UpdateUserInfoAsync(userDTO);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.NotNull(result.Result);
            Assert.Equal(userDTO.Id, result.Result.Id);
            Assert.Equal(userDTO.UserName, result.Result.UserName);
            Assert.Equal(userDTO.PhoneNumber, result.Result.PhoneNumber);
            Assert.Equal(userDTO.AddressDelivery, result.Result.AddressDelivery);
            Assert.Equal(userDTO.ConcurrencyStamp, result.Result.ConcurrencyStamp);
        }

        [Fact]
        public async Task ShouldNotUpdateUserInfoNotFoundUser_ReturnServiceResultClassWithUserDTO()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var userDTO = mapper.Map<UserDTO>(user);

            A.CallTo(() => userRepository.UpdateUserInfoAsync(A<UserDTO>._)).DoesNothing();
            A.CallTo(() => userManager.FindByIdAsync(user.Id.ToString())).Returns((ApplicationUser)null);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.UpdateUserInfoAsync(userDTO);

            //Assert
            Assert.Equal(ServiceResultType.Bad_Request, result.ServiceResultType);
            Assert.Null(result.Result);

            A.CallTo(() => redisContext.Remove<ApplicationUser>(A<string>._, A<TimeSpan>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ShouldChangePasswordByResetPasswordUserDTOWithRedis_ReturnServiceResult()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var resetPasswordDTO = TestValues.TestRsetPasswordUserDTO;

            A.CallTo(() => redisContext.Get<ApplicationUser>(A<string>._)).Returns(user);
            A.CallTo(() => userRepository.UpdatePasswordAsync(resetPasswordDTO.Id, resetPasswordDTO.OldPassword, resetPasswordDTO.NewPassword)).DoesNothing();

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.ChangePasswordAsync(resetPasswordDTO);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);

            A.CallTo(() => userManager.FindByIdAsync(TestValues.TestId)).MustNotHaveHappened();
            A.CallTo(() => userRepository.UpdatePasswordAsync(resetPasswordDTO.Id, resetPasswordDTO.OldPassword, resetPasswordDTO.NewPassword)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ShouldChangePasswordByResetPasswordUserDTOWithUserManager_ReturnServiceResult()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var resetPasswordDTO = TestValues.TestRsetPasswordUserDTO;

            A.CallTo(() => redisContext.Get<ApplicationUser>(A<string>._)).Returns((ApplicationUser)null);
            A.CallTo(() => userManager.FindByIdAsync(TestValues.TestId)).Returns(user);
            A.CallTo(() => userRepository.UpdatePasswordAsync(resetPasswordDTO.Id, resetPasswordDTO.OldPassword, resetPasswordDTO.NewPassword)).DoesNothing();

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.ChangePasswordAsync(resetPasswordDTO);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);

            A.CallTo(() => userRepository.UpdatePasswordAsync(resetPasswordDTO.Id, resetPasswordDTO.OldPassword, resetPasswordDTO.NewPassword)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ShouldNotChangePasswordByResetPasswordUserDTOWithUserManager_ReturnServiceResult()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var resetPasswordDTO = TestValues.TestRsetPasswordUserDTO;

            A.CallTo(() => redisContext.Get<ApplicationUser>(A<string>._)).Returns((ApplicationUser)null);
            A.CallTo(() => userManager.FindByIdAsync(A<string>._)).Returns((ApplicationUser)null);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.ChangePasswordAsync(resetPasswordDTO);

            //Assert
            Assert.Equal(ServiceResultType.Bad_Request, result.ServiceResultType);

            A.CallTo(() => userRepository.UpdatePasswordAsync(resetPasswordDTO.Id, resetPasswordDTO.OldPassword, resetPasswordDTO.NewPassword)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ShouldFindUserByIdWithRedis_ReturnServiceResultClassWithUserDTO()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var userDTO = mapper.Map<UserDTO>(user);

            A.CallTo(() => redisContext.Get<ApplicationUser>(A<string>._)).Returns(user);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.FindUserByIdAsync(user.Id);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.NotNull(result.Result);
            Assert.Equal(userDTO.Id, result.Result.Id);
            Assert.Equal(userDTO.UserName, result.Result.UserName);
            Assert.Equal(userDTO.PhoneNumber, result.Result.PhoneNumber);
            Assert.Equal(userDTO.AddressDelivery, result.Result.AddressDelivery);
            Assert.Equal(userDTO.ConcurrencyStamp, result.Result.ConcurrencyStamp);

            A.CallTo(() => userRepository.GetUserByIdAsync(user.Id)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ShouldFindUserByIdWithUserManager_ReturnServiceResultClassWithUserDTO()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var userDTO = mapper.Map<UserDTO>(user);

            A.CallTo(() => redisContext.Get<ApplicationUser>(A<string>._)).Returns((ApplicationUser)null);
            A.CallTo(() => userRepository.GetUserByIdAsync(user.Id)).Returns(userDTO);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.FindUserByIdAsync(user.Id);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.NotNull(result.Result);
            Assert.Equal(userDTO.Id, result.Result.Id);
            Assert.Equal(userDTO.UserName, result.Result.UserName);
            Assert.Equal(userDTO.PhoneNumber, result.Result.PhoneNumber);
            Assert.Equal(userDTO.AddressDelivery, result.Result.AddressDelivery);
            Assert.Equal(userDTO.ConcurrencyStamp, result.Result.ConcurrencyStamp);
        }

        [Fact]
        public async Task ShouldNotFindUserByIdr_ReturnServiceResultClassWithUserDTO()
        {
            //Arrange
            var store = A.Fake<IUserStore<ApplicationUser>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>();
            var signInManager = A.Fake<SignInManager<ApplicationUser>>();
            var roleManager = A.Fake<RoleManager<ApplicationRole>>();
            var jwtGenerator = new JwtGenerator(appSettings);
            var userRepository = A.Fake<IUserRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var tokenEncodingHelper = new TokenEncodingHelper();
            var redisContext = A.Fake<IRedisContext>();

            var user = TestValues.TestUser;
            var userDTO = mapper.Map<UserDTO>(user);

            A.CallTo(() => redisContext.Get<ApplicationUser>(A<string>._)).Returns((ApplicationUser)null);
            A.CallTo(() => userRepository.GetUserByIdAsync(user.Id)).Returns((UserDTO)null);

            var userService = new UserService(userManager, signInManager, roleManager, jwtGenerator, userRepository, mapper, tokenEncodingHelper, redisContext);

            //Act
            var result = await userService.FindUserByIdAsync(user.Id);

            //Assert
            Assert.Equal(ServiceResultType.Not_Found, result.ServiceResultType);
            Assert.Null(result.Result);
        }
    }
}
