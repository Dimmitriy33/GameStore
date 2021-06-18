using System.Collections.Generic;

namespace WebApp.Web.Startup.Settings
{
    public class IdentitySettings
    {
        public ICollection<string> Roles { get; set; } = new List<string>();
        public bool SignInRequireConfirmedEmail { get; set; }
        public bool PasswordRequireDigit { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
    }
}
