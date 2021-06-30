using System.Collections.Generic;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.Interfaces.Database;

namespace WebApp.BLL.Services
{
    public class ProductService : IProductService
    {
        //constants
        private const string NotFoundPlatforms = "Platforms not found";


        //services
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ServiceResultClass<List<string>> GetTopThreePlatforms()
        {
            var platforms = _productRepository.GetTopThreePopularPlatforms();

            if(platforms is null)
            {
                return new ServiceResultClass<List<string>>(NotFoundPlatforms,ServiceResultType.Internal_Server_Error);
            }

            List<string> list = new List<string>();
            foreach(var pl in platforms)
            {
                list.Add(pl.ToString());
            }

            return new ServiceResultClass<List<string>>(list, ServiceResultType.Success);
        }
    }
}
