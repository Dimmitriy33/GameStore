using System;

namespace WebApp.BLL.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(Guid userId, string userName, string userRole);
    }
}
