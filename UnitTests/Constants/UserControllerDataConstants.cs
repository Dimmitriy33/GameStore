using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Security.Claims;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;

namespace UnitTests.Constants
{
    public static class UserControllerDataConstants
    {
        public static ClaimsPrincipal GetUserIdentity(
            string userId = UserConstants.TestId,
            string userName = UserConstants.TestUsername,
            string userRole = RolesConstants.User)
        {
            var userClaims = new ClaimsPrincipal(
                new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.NameIdentifier, userId.ToString()),
                    new (ClaimTypes.Role, userRole),
                    new (ClaimTypes.Name, userName)
                })
            );

            return userClaims;
        }

        public static JsonPatchDocument<ResetPasswordUserDTO> GetJsonPatchDocumentForResetPassword(
            string userId = UserConstants.TestId,
            string oldPassword = UserConstants.TestPassword1,
            string newPassword = UserConstants.TestPassword2)
        {
            var jsonPatch = new JsonPatchDocument<ResetPasswordUserDTO>();

            jsonPatch.Replace(j => j.Id, new Guid(userId));
            jsonPatch.Replace(j => j.OldPassword, oldPassword);
            jsonPatch.Replace(j => j.NewPassword, newPassword);

            return jsonPatch;
        }
    }
}
