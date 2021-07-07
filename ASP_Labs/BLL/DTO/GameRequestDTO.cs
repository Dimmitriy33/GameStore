using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApp.BLL.Constants;
using WebApp.BLL.ValidationAttributes;

namespace WebApp.BLL.DTO
{
    public class GameRequestDTO : BaseGameModel
    {
        /// <summary>
        /// Game logotype uploaded file
        /// </summary>
        [Required]
        [FormFileSize(FilesConstants.defaultMinFileSize, FilesConstants.defaultMaxFileSize)]
        public IFormFile Logo { get; set; }

        /// <summary>
        /// Game background uploaded file
        /// </summary>
        [Required]
        [FormFileSize(FilesConstants.defaultMinFileSize, FilesConstants.defaultMaxFileSize)]
        public IFormFile Background { get; set; }

    }
}
