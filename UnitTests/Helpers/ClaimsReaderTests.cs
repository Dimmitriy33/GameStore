using System;
using System.Security.Claims;
using UnitTests.Constants;
using WebApp.BLL.Constants;
using WebApp.BLL.Helpers;
using WebApp.BLL.Models;
using Xunit;

namespace UnitTests.Helpers
{
    public class ClaimsReaderTests
    {
        [Fact]
        public void Get_UserIdPositive_ReturnServiceResultStructObject()
        {
            //Arrange
            var userId = TestValues.TestId;
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

            var claimsReader = new ClaimsReader();

            //Act
            var result = claimsReader.GetUserId(user);

            //Assert
            Assert.True(result.Result != Guid.Empty);
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
        }

        [Fact]
        public void Get_UserIdNegative_ReturnServiceResultStructObject()
        {
            //Arrange
            var userRole = RolesConstants.User;
            var userName = TestValues.TestUsername;

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, string.Empty),
                    new Claim(ClaimTypes.Role, userRole),
                    new Claim(ClaimTypes.Name, userName),
                },
                "Token")
            );

            var claimsReader = new ClaimsReader();

            //Act
            var result = claimsReader.GetUserId(user);

            //Assert
            Assert.Equal(ServiceResultType.Bad_Request, result.ServiceResultType);
            Assert.True(result.Result == Guid.Empty);
        }
    }
}
