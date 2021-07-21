using AutoMapper;
using WebApp.BLL.DTO;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Mappers
{
    public class ProductRatingProfile : Profile
    {
        public ProductRatingProfile()
        {
            CreateMap<ProductRating, ProductRatingDTO>().ReverseMap();
        }
    }
}
