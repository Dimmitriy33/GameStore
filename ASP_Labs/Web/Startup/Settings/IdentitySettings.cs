using NetEscapades.Configuration.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Web.Startup.Settings
{
    public class IdentitySettings : IValidatable
    {
        public ICollection<string> Roles { get; set; } = new List<string>();
        public bool SignInRequireConfirmedEmail { get; set; }
        public bool PasswordRequireDigit { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
