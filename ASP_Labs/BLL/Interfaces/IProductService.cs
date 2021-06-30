using System.Collections.Generic;
using WebApp.BLL.Models;

namespace WebApp.BLL.Interfaces
{
    public interface IProductService
    {
        ServiceResultClass<List<string>> GetTopThreePlatforms();
    }
}
