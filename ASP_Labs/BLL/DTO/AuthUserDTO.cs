using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.DTO
{
    public class AuthUserDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
