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
        public int Port { get; set; }
        public string ConnectionString => $"Server=tcp:{Host},{Port};Database={Instance};User={Username};Password={Password};MultipleActiveResultSets=True;";
        public void Validate()
            => Validator.ValidateObject(this, new ValidationContext(this), true);
    }
}
