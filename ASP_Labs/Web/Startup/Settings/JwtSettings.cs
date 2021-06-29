using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Web.Startup.Settings
{
    public class JwtSettings : IValidatable
    {
        [Required]
        public string TokenKey { get; set; }
        [Required]
        public string Issuer { get; set; }
        [Required]
        public string Audience { get; set; }
        [Range(1, double.MaxValue)]
        public double Lifetime { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);

        }
    }
}
