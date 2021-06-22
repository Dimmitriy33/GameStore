using AutoMapper;
using WebApp.BLL.DTO;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.UserName))
                .ForMember(
                dest => dest.ConcurrencyStamp,
                opt => opt.MapFrom(src => src.ConcurrencyStamp))
                .ForMember(
                dest => dest.AddressDelivery,
                opt => opt.MapFrom(src => src.AddressDelivery))
                .ForMember(
                dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber)
                )
                .ReverseMap();
        }
    }
}
