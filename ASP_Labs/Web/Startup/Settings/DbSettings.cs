using System.ComponentModel.DataAnnotations;

namespace WebApp.Web.Startup.Settings
{
    public class DbSettings
    {
        [Required]
        public string Host { get; set; }
        [Required]
        public string Instance { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string ConnectionString => $"Server={Host};Database={Instance};User={Username};Password={Password};";
        public void Validate() => Validator.ValidateObject(this, new ValidationContext(this), true);
    }
}
