using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;
using WebApp.Web.Validation;

namespace WebApp.Web.Startup.Settings
{
    public class JwtSettings : IValidatable
    {
        [DefaultValue]
        public string TokenKey { get; set; }
        [DefaultValue]
        public string Issuer { get; set; }
        [DefaultValue]
        public string Audience { get; set; }
        [DefaultValue]
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
