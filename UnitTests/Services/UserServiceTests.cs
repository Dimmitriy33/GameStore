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
    }
}
