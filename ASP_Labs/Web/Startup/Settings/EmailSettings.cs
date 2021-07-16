using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Web.Startup.Settings
{
    public class EmailSettings : IValidatable
    {
        [Required, EmailAddress]
        public string DefaultEmail { get; set; }
        [Required, DataType(DataType.Password)]
        public string DefaultPassword { get; set; }
        [Required]
        public string DefaultName { get; set; }
        [Required]
        public string DefaultSMTPServer { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int DefaultSMTPServerPort { get; set; }

        public void Validate()
            => Validator.ValidateObject(this, new ValidationContext(this), true);
    }
}
