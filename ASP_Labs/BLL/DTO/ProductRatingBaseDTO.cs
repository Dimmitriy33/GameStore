using System;

namespace WebApp.BLL.DTO
{
    public class ProductRatingBaseDTO
    {
        /// <summary>
        /// Games id
        /// </summary>
        /// <example>01111d6e-ed62-482b-a4d9-335dfa68d58e</example>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Game rating from user
        /// </summary>
        /// <example>1</example>
        public double Rating { get; set; }
    }
}
