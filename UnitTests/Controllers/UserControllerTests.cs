using AutoFixture;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using UnitTests.Constants;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.BLL.Helpers;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.Web.Controllers;
using Xunit;

namespace UnitTests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task Get_UserPositive_ReturnsUserDTO()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();

            var userId = new Guid(TestValues.TestId);
            var userRole = RolesConstants.User;
            var userName = TestValues.TestUsername;

            var userDTO = new ServiceResultClass<UserDTO>()
            {
                Result = new UserDTO
                {
                    UserName = userName,
                    Id = userId,
                    AddressDelivery = TestValues.TestAddressDelivery,
                    PhoneNumber = TestValues.TestPhoneNumber,
                    ConcurrencyStamp = TestValues.TestConcurrencyStamp
                },
                ServiceResultType = ServiceResultType.Success
            };

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, userRole),
                    new Claim(ClaimTypes.Name, userName),
                },
                "Token")
            );

            var httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var userController = new UserController(userService, claimsReader)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextAccessor.HttpContext
                }
            };

            A.CallTo(() => userService.FindUserByIdAsync(userId)).Returns(userDTO);

            //Act
            var result = await userController.GetUser();

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(actionResult.Value);
            Assert.Equal(userDTO.Result.UserName, ((UserDTO)actionResult.Value).UserName);
            Assert.Equal(userDTO.Result.AddressDelivery, ((UserDTO)actionResult.Value).AddressDelivery);
            Assert.Equal(userDTO.Result.PhoneNumber, ((UserDTO)actionResult.Value).PhoneNumber);
            Assert.Equal(userDTO.Result.ConcurrencyStamp, ((UserDTO)actionResult.Value).ConcurrencyStamp);
            Assert.Equal(userDTO.Result.Id.ToString(), ((UserDTO)actionResult.Value).Id.ToString());
        }

        [Fact]
        public async void Get_UserNegative_ReturnBadRequest()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();

            var userId = new Guid(TestValues.TestId);
            var userRole = RolesConstants.User;
            var userName = TestValues.TestUsername;
            var userDTO = new ServiceResultClass<UserDTO>
            {
                Result = new UserDTO(),
                ServiceResultType = ServiceResultType.Bad_Request
            };

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, userRole),
                    new Claim(ClaimTypes.Name, userName),
                },
                "Token")
            );

            var httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var userController = new UserController(userService, claimsReader)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextAccessor.HttpContext
                }
            };

            A.CallTo(() => userService.FindUserByIdAsync(userId)).Returns(userDTO);

            //Act
            var result = await userController.GetUser();

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void Get_UserIdFromClaimsNegative_ReturnBadRequest()
        {
            //Arrange
            var claimsReader = new ClaimsReader();

            var userId = string.Empty;
            var userRole = RolesConstants.User;
            var userName = TestValues.TestUsername;

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Role, userRole),
                    new Claim(ClaimTypes.Name, userName),
                },
                "Token")
            );

            //Act
            var result = claimsReader.GetUserId(user);

            //Assert
            Assert.Equal(ServiceResultType.Bad_Request, result.ServiceResultType);
        }

        [Fact]
        public async Task Update_UserPositive_ReturnsUserDTO()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();

            var userForUpdate = new Fixture().Create<UserDTO>();

            var userDTO = new ServiceResultClass<UserDTO>
            {
                Result = userForUpdate,
                ServiceResultType = ServiceResultType.Success
            };

            var userController = new UserController(userService, claimsReader);

            A.CallTo(() => userService.UpdateUserInfoAsync(userForUpdate)).Returns(userDTO);

            //Act
            var result = await userController.Update(userForUpdate);

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(actionResult.Value);
            Assert.Equal(userDTO.Result.UserName, ((UserDTO)actionResult.Value).UserName);
            Assert.Equal(userDTO.Result.AddressDelivery, ((UserDTO)actionResult.Value).AddressDelivery);
            Assert.Equal(userDTO.Result.PhoneNumber, ((UserDTO)actionResult.Value).PhoneNumber);
            Assert.Equal(userDTO.Result.ConcurrencyStamp, ((UserDTO)actionResult.Value).ConcurrencyStamp);
            Assert.Equal(userDTO.Result.Id.ToString(), ((UserDTO)actionResult.Value).Id.ToString());
        }

        [Fact]
        public async Task Update_UserNegative_ReturnBadRequest()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();
            var userDTO = new ServiceResultClass<UserDTO>
            {
                ServiceResultType = ServiceResultType.Bad_Request
            };

            var userForUpdate = new Fixture().Create<UserDTO>();

            var userController = new UserController(userService, claimsReader);

            A.CallTo(() => userService.UpdateUserInfoAsync(userForUpdate)).Returns(userDTO);

            //Act
            var result = await userController.Update(userForUpdate);

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_UserPasswordPositive_ReturnOkResult()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();

            var resultData = new ServiceResult(string.Empty, ServiceResultType.Success);

            var jsonPatch = new JsonPatchDocument<ResetPasswordUserDTO>();
            jsonPatch.Replace(j => j.Id, new Guid(TestValues.TestId));
            jsonPatch.Replace(j => j.OldPassword, TestValues.TestPassword1);
            jsonPatch.Replace(j => j.NewPassword, TestValues.TestPassword2);

            var userController = new UserController(userService, claimsReader);

            A.CallTo(() => userService.ChangePasswordAsync(A<ResetPasswordUserDTO>._)).Returns(resultData);

            //Act
            var result = await userController.ChangePassword(jsonPatch);

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Update_UserPasswordNegative_ReturnBadRequest()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();

            var resultData = new ServiceResult(string.Empty, ServiceResultType.Bad_Request);

            var jsonPatch = new JsonPatchDocument<ResetPasswordUserDTO>();
            jsonPatch.Replace(j => j.Id, new Guid(TestValues.TestId));
            jsonPatch.Replace(j => j.OldPassword, string.Empty);
            jsonPatch.Replace(j => j.NewPassword, string.Empty);

            var userController = new UserController(userService, claimsReader);

            A.CallTo(() => userService.ChangePasswordAsync(A<ResetPasswordUserDTO>._)).Returns(resultData);

            //Act
            var result = await userController.ChangePassword(jsonPatch);

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
