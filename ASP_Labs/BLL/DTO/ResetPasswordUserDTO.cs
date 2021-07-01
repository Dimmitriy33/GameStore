using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.DTO
{
    public class ResetPasswordUserDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}
