using System;
using System.ComponentModel.DataAnnotations;
using WebApp.DAL.Enums;

namespace WebApp.BLL.DTO
{
    public abstract class BaseGameModel
    {
        /// <summary>
        /// Game unique identifier
        /// </summary>
        /// <example>01111d6e-ed62-482b-a4d9-335dfa68d58e</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Game name
        /// </summary>
        /// <example>BraveStas</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Game platform
        /// </summary>
        /// <example>1 or PC</example>
        [EnumDataType(typeof(Platforms))]
        public Platforms Platform { get; set; }

        /// <summary>
        /// Game rating
        /// </summary>
        /// <example>10</example>
        [Range(0.0,10)]
        public double TotalRating { get; set; }

        /// <summary>
        /// Game genre
        /// </summary>
        /// <example>MMO</example>
        [EnumDataType(typeof(GamesGenres))]
        public GamesGenres Genre { get; set; }

        /// <summary>
        /// Game rating by age
        /// </summary>
        /// <example>6</example>
        [EnumDataType(typeof(GamesRating))]
        public GamesRating Rating { get; set; }

        /// <summary>
        /// Game logotype image
        /// </summary>

        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Game count
        /// </summary>
        /// <example>1</example>
        public int Count { get; set; }

        /// <summary>
        /// Is this game not available for users
        /// </summary>
        /// <example>true</example>
        public bool IsDeleted { get; set; }
    }
}
