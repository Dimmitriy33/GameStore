using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace WebApp.BLL.Helpers
{
    public static class ClaimsHelper
    {
        public static string GetUserId(ClaimsPrincipal user) => user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
    }
}
