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
            CreateMap<Product, GameDTO>().ReverseMap();
        }
    }
}
