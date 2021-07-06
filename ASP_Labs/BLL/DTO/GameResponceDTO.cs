using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.DTO
{
    public class GameResponceDTO : GameDTO
    {
        [Required]
        public string Logo { get; set; }

        /// <summary>
        /// Game background image
        /// </summary>
        [Required]
        public string Background { get; set; }

        /// <summary>
        /// Game price
        /// </summary>
        /// <example>20.5</example>
    }
}
