using AutoMapper;
using WebApp.BLL.DTO;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GameResponseDTO, Product>().ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<GameRequestDTO, Product>().ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<Product, GameResponseDTO>();
        }
    }
}
