using System;
using System.Collections.Generic;
using WebApp.BLL.DTO;
using WebApp.DAL.Entities;

namespace UnitTests.Constants
{
    public static class UserConstants
    {
        public const string TestId = "37b73959-85cd-41e8-4251-08d945d5ba96";
        public const string TestUsername = "string";
        public const string TestEmail = "test@gmail.com";
        public const string TestAddressDelivery = "ulica Pervaya, dom 21";
        public const string TestPhoneNumber = "+375292929292";
        public const string TestConcurrencyStamp = "6f8ad6e6-653a-45ed-b784-9ab63141732b";
        public const string TestPassword1 = "Skolko1000minus7";
        public const string TestPassword2 = "SkolkoTisyachaMinus7";

        public static readonly Guid TestGuid1 = Guid.Parse("a76d6bde-c48c-4dcb-b80a-7c6edce28c74");
        public static readonly Guid TestGuid2 = Guid.Parse("37b73959-85cd-41e8-4251-08d945d5ba96");

        public static List<Guid> TestGuidList = new()
        {
            TestGuid1,
            TestGuid2
        };
        public static readonly ApplicationUser TestUser = new()
        {
            Id = TestGuid1,
            UserName = TestUsername,
            AddressDelivery = TestAddressDelivery,
            PhoneNumber = TestPhoneNumber,
            ConcurrencyStamp = TestConcurrencyStamp
        };

        public static readonly UserDTO TestUserDTO = new()
        {
            Id = TestGuid1,
            UserName = TestUsername,
            AddressDelivery = TestAddressDelivery,
            PhoneNumber = TestPhoneNumber,
            ConcurrencyStamp = TestConcurrencyStamp
        };

        public static readonly ResetPasswordUserDTO TestResetPasswordUserDTO = new()
        {
            Id = TestGuid1,
            NewPassword = TestPassword1,
            OldPassword = TestPassword2
        };

        public static SignUpUserDTO SignUpUser = new()
        {
            AddressDelivery = TestAddressDelivery,
            UserName = TestUsername,
            Email = TestEmail,
            Password = TestPassword1,
            PhoneNumber = TestPhoneNumber
        };

        public static SignInUserDTO SignInUser = new()
        {
            Email = TestEmail,
            Password = TestPassword1
        };
    }
}
