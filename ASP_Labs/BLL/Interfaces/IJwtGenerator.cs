using WebApp.DAL.Entities;

namespace WebApp.BLL.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(ApplicationUser user);
    }
}
