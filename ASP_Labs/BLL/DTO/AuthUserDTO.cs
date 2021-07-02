using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.DTO
{
    public class AuthUserDTO
    {
        /// <summary>
        /// User Email
        /// </summary>
        /// <example>string@gmail.com</example>
        [Required, EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        /// <example>HelloMyNameIsZuzi228</example>
        [Required, StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
