using System.Collections.Generic;

namespace WebApp.Web.Startup.Settings
{
    public class IdentitySettings
    {
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
