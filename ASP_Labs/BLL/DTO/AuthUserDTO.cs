using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.DTO
{
    public class AuthUserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
