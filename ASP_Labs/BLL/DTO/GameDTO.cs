using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using WebApp.BLL.Constants;
using WebApp.DAL.Enums;

namespace WebApp.BLL.DTO
{
    public class GameDTO
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
        public GamesGenres Genre { get; set; }

        /// <summary>
        /// Game rating by age
        /// </summary>
        /// <example>6</example>
        public GamesRating Rating { get; set; }

        /// <summary>
        /// Game logotype image
        /// </summary>
        [Required]
        [MinLength(FilesConstants.defaultMinFileSize), MaxLength(FilesConstants.defaultMaxFileSize)]
        public IFormFile Logo { get; set; }

        /// <summary>
        /// Game background image
        /// </summary>
        [Required]
        [MinLength(FilesConstants.defaultMinFileSize), MaxLength(FilesConstants.defaultMaxFileSize)]
        public IFormFile Background { get; set; }

        /// <summary>
        /// Game price
        /// </summary>
        /// <example>20.5</example>

        [Range(0.0, Double.MaxValue)]
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
