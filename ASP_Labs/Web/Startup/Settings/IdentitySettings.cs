using NetEscapades.Configuration.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Web.Startup.Settings
{
    public class IdentitySettings : IValidatable
    {
        [Required]
        public ICollection<string> Roles { get; set; } = new List<string>();
        [Required]
        public bool SignInRequireConfirmedEmail { get; set; }
        [Required]
        public bool PasswordRequireDigit { get; set; }
        [Required]
        public bool PasswordRequireNonAlphanumeric { get; set; }
        [Required]
        public bool PasswordRequireUppercase { get; set; }
        [Required]
        public bool PasswordRequireLowercase { get; set; }
        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
