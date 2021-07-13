using AutoMapper;
using WebApp.BLL.DTO;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<GameResponseDTO, Product>().ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<GameRequestDTO, Product>().ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<Product, GameResponseDTO>();
            CreateMap<ProductRating, ProductRatingDTO>().ReverseMap();
            CreateMap<OrderItemDTO, Order>().ReverseMap();
        }
    }
}
