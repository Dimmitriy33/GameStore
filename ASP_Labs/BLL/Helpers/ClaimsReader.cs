using System;
using System.Linq;
using System.Security.Claims;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;

namespace WebApp.BLL.Helpers
{
    public class ClaimsReader : IClaimsReader
    {
        public ServiceResultStruct<Guid> GetUserId(ClaimsPrincipal user) =>
            !Guid.TryParse(user.Claims.FirstOrDefault(c => c.Type is ClaimTypes.NameIdentifier)?.Value, out var userId)
                ? new ServiceResultStruct<Guid>(userId, ServiceResultType.Bad_Request)
                : new ServiceResultStruct<Guid>(userId, ServiceResultType.Success);
    }
}
