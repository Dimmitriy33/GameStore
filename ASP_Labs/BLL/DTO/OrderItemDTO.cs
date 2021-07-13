using System;

namespace WebApp.BLL.DTO
{
    public class OrderItemDTO : OrderGameDTO
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
