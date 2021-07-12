using System;

namespace WebApp.BLL.DTO
{
    public class ProductRatingDTO : ProductRatingActionDTO
    {
        /// <summary>
        /// User id
        /// </summary>
        /// <example>01111d6e-ed62-482b-a4d9-335dfa68d58e</example>
        public Guid UserId { get; set; }
    }
}
