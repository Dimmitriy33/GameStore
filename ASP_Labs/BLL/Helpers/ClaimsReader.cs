using System;
using System.Security.Claims;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;

namespace WebApp.BLL.Helpers
{
    public class ClaimsReader : IClaimsReader
    {
        public ServiceResult<Guid> GetUserId(ClaimsPrincipal user) =>
            Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
                ? new ServiceResult<Guid>(userId, ServiceResultType.Success)
                : new ServiceResult<Guid>(ServiceResultType.BadRequest);
    }
}
