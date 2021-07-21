using System;
using UnitTests.Constants;
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
            var user = UserControllerDataConstants.GetUserIdentity();

            var claimsReader = new ClaimsReader();

            //Act
            var result = claimsReader.GetUserId(user);

            //Assert
            Assert.NotEqual(Guid.Empty, result.Result);
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
        }

        [Fact]
        public void Get_UserIdNegative_ReturnServiceResultStructObject()
        {
            //Arrange
            var user = UserControllerDataConstants.GetUserIdentity(string.Empty);

            var claimsReader = new ClaimsReader();

            //Act
            var result = claimsReader.GetUserId(user);

            //Assert
            Assert.Equal(ServiceResultType.BadRequest, result.ServiceResultType);
            Assert.Equal(Guid.Empty, result.Result);
        }
    }
}
