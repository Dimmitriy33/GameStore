using System;

namespace WebApp.DAL.Entities
{
    public class ProductRating
    {
        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public double Rating { get; set; }
    }
}
