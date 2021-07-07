using AutoMapper;
using System.Collections.Generic;
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
            CreateMap<IEnumerable<Product>, IEnumerable<GameResponseDTO>>().ReverseMap();
            CreateMap<Product, GameResponseDTO>();
        }
    }
}
