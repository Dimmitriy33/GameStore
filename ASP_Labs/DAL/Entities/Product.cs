using System;
using System.Collections.Generic;
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
        public GamesGenres Genre { get; set; }
        public GamesRating Rating { get; set; }
        public string Logo { get; set; }
        public string Background { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
    }
}
