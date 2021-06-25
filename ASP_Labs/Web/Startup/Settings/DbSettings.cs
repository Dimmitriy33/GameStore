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
        public string Password_hash { get; set; }
        public string ConnectionString => $"Server={Host};Database={Instance};User={Username};Password={Password_hash};integrated security=True;Trusted_Connection=True;MultipleActiveResultSets=true";
        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
