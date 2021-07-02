using System;
using System.Security.Claims;
using WebApp.BLL.Models;

namespace WebApp.BLL.Interfaces
{
    public interface IClaimsReader
    {
        public ServiceResultStruct<Guid> GetUserId(ClaimsPrincipal user);
    }
}
