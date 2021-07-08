using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Web.Startup.Settings
{
    public class CloudinarySettings : IValidatable
    {
        [Required]
        public string CloudName { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string ApiSecret { get; set; }

        public string DefaultCloudinaryFolder { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
