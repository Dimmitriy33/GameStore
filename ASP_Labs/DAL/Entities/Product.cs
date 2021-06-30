using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.DAL.Enums;

namespace WebApp.DAL.Entities
{
    [Table("Products")]
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Platforms Platform { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal TotalRating { get; set; }
    }
}
