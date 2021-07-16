using System;
using WebApp.DAL.Enums;

namespace WebApp.DAL.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Amount { get; set; }
        public OrderStatus Status { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
