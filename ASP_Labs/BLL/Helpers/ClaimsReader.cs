using System;
using System.Security.Claims;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;

namespace WebApp.BLL.Helpers
{
    public class ClaimsReader : IClaimsReader
    {
        public ServiceResultStruct<Guid> GetUserId(ClaimsPrincipal user) =>
            Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
                ? new ServiceResultStruct<Guid>(userId, ServiceResultType.Success)
                : new ServiceResultStruct<Guid>(ServiceResultType.BadRequest);
    }
}
