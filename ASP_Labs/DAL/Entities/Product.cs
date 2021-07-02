using System;
using WebApp.DAL.Enums;

namespace WebApp.DAL.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Platforms Platform { get; set; }
        public DateTime DateCreated { get; set; }
        public double TotalRating { get; set; }
    }
}
