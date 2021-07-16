using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.Web.Controllers;
using Xunit;

namespace UnitTests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task ShouldGetUser()
        {
            //Arrange
            var claimsReader = A.Fake<IClaimsReader>();
            var userService = A.Fake<IUserService>();

            var userId = new Guid("37b73959-85cd-41e8-4251-08d945d5ba96");
            var userRole = RolesConstants.User;
            var userName = "string";
            var userDTO = new ServiceResultClass<UserDTO>();

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, userRole.ToString()),
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

        }

    }
}
