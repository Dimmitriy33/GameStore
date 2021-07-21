using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using UnitTests.Constants;
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

            var ServiceResultUserDTO = new ServiceResult<UserDTO>(UserConstants.TestUserDTO, ServiceResultType.Success);

            var httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext
                {
                    User = UserControllerDataConstants.GetUserIdentity()
                }
            };

            var userController = new UserController(userService, claimsReader)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextAccessor.HttpContext
                }
            };

            A.CallTo(() => userService.FindUserByIdAsync(A<Guid>._)).Returns(ServiceResultUserDTO);

            //Act
            var result = await userController.GetUser();

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(actionResult.Value);

            AssertUsertDTOProperties(ServiceResultUserDTO.Result, (UserDTO)actionResult.Value);

            A.CallTo(() => userService.FindUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Get_UserNegative_ReturnBadRequest()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();

            var ServiceResultUserDTO = new ServiceResult<UserDTO>(UserConstants.TestUserDTO, ServiceResultType.BadRequest);

            var httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext
                {
                    User = UserControllerDataConstants.GetUserIdentity()
                }
            };

            var userController = new UserController(userService, claimsReader)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextAccessor.HttpContext
                }
            };

            A.CallTo(() => userService.FindUserByIdAsync(A<Guid>._)).Returns(ServiceResultUserDTO);

            //Act
            var result = await userController.GetUser();

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);

            A.CallTo(() => userService.FindUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Get_UserIdFromClaimsNegative_ReturnBadRequest()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var user = UserControllerDataConstants.GetUserIdentity(string.Empty);

            //Act
            var result = claimsReader.GetUserId(user);

            //Assert
            Assert.Equal(ServiceResultType.BadRequest, result.ServiceResultType);
        }

        [Fact]
        public async Task Update_UserPositive_ReturnsUserDTO()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();

            var ServiceResultUserDTO = new ServiceResult<UserDTO>(UserConstants.TestUserDTO, ServiceResultType.Success);

            var userController = new UserController(userService, claimsReader);

            A.CallTo(() => userService.UpdateUserInfoAsync(A<UserDTO>._)).Returns(ServiceResultUserDTO);

            //Act
            var result = await userController.Update(UserConstants.TestUserDTO);

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(actionResult.Value);

            AssertUsertDTOProperties(ServiceResultUserDTO.Result, (UserDTO)actionResult.Value);

            A.CallTo(() => userService.UpdateUserInfoAsync(A<UserDTO>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_UserNegative_ReturnBadRequest()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();
            var ServiceResultUserDTO = new ServiceResult<UserDTO>(ServiceResultType.BadRequest);

            var userController = new UserController(userService, claimsReader);

            A.CallTo(() => userService.UpdateUserInfoAsync(A<UserDTO>._)).Returns(ServiceResultUserDTO);

            //Act
            var result = await userController.Update(UserConstants.TestUserDTO);

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);

            A.CallTo(() => userService.UpdateUserInfoAsync(A<UserDTO>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_UserPasswordPositive_ReturnOkResult()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();

            var resultData = new ServiceResult(string.Empty, ServiceResultType.Success);
            var jsonPatch = UserControllerDataConstants.GetJsonPatchDocumentForResetPassword();

            var userController = new UserController(userService, claimsReader);

            A.CallTo(() => userService.ChangePasswordAsync(A<ResetPasswordUserDTO>._)).Returns(resultData);

            //Act
            var result = await userController.ChangePassword(jsonPatch);

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);

            A.CallTo(() => userService.ChangePasswordAsync(A<ResetPasswordUserDTO>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_UserPasswordNegative_ReturnBadRequest()
        {
            //Arrange
            var claimsReader = new ClaimsReader();
            var userService = A.Fake<IUserService>();

            var resultData = new ServiceResult(string.Empty, ServiceResultType.BadRequest);

            var jsonPatch = UserControllerDataConstants.GetJsonPatchDocumentForResetPassword(oldPassword: string.Empty, newPassword: string.Empty);

            var userController = new UserController(userService, claimsReader);

            A.CallTo(() => userService.ChangePasswordAsync(A<ResetPasswordUserDTO>._)).Returns(resultData);

            //Act
            var result = await userController.ChangePassword(jsonPatch);

            //Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.True(actionResult.StatusCode.HasValue);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);

            A.CallTo(() => userService.ChangePasswordAsync(A<ResetPasswordUserDTO>._)).MustHaveHappenedOnceExactly();
        }

        private static void AssertUsertDTOProperties(UserDTO expectedUserDTO, UserDTO actualUserDTO)
        {
            Assert.Equal(expectedUserDTO.Id.ToString(), actualUserDTO.Id.ToString());
            Assert.Equal(expectedUserDTO.UserName, actualUserDTO.UserName);
            Assert.Equal(expectedUserDTO.PhoneNumber, actualUserDTO.PhoneNumber);
            Assert.Equal(expectedUserDTO.AddressDelivery, actualUserDTO.AddressDelivery);
            Assert.Equal(expectedUserDTO.ConcurrencyStamp, actualUserDTO.ConcurrencyStamp);
        }
    }
}
