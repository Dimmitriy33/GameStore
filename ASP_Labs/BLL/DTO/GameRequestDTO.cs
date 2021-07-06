using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApp.BLL.Constants;
using WebApp.BLL.ValidationAttributes;

namespace WebApp.BLL.DTO
{
    public class GameRequestDTO : GameDTO
    {
        [Required]
        [FormFileSize(FilesConstants.defaultMinFileSize,FilesConstants.defaultMaxFileSize)]
        public IFormFile Logo { get; set; }

        /// <summary>
        /// Game background image
        /// </summary>
        [Required]
        [FormFileSize(FilesConstants.defaultMinFileSize, FilesConstants.defaultMaxFileSize)]
        public IFormFile Background { get; set; }

        /// <summary>
        /// Game price
        /// </summary>
        /// <example>20.5</example>
    }
}
