using System;
using System.Security.Claims;
using WebApp.BLL.Models;

namespace WebApp.BLL.Interfaces
{
    public interface IClaimsReader
    {
        public ServiceResult<Guid> GetUserId(ClaimsPrincipal user);
    }
}
