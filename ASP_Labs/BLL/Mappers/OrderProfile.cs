using AutoMapper;
using WebApp.BLL.DTO;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItemDTO, Order>().ReverseMap();
        }
    }
}
