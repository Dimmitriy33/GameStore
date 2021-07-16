using NetEscapades.Configuration.Validation;

namespace WebApp.Web.Startup.Settings
{
    public class AppSettings : IValidatable
    {
        public DbSettings DbSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }
        public EmailSettings EmailSettings { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public CloudinarySettings CloudinarySettings { get; set; }

        public void Validate()
        {
            DbSettings.Validate();
            EmailSettings.Validate();
            IdentitySettings.Validate();
            JwtSettings.Validate();
            CloudinarySettings.Validate();
        }
    }
}
