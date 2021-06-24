namespace WebApp.Web.Startup.Settings
{
    public class JwtSettings
    {
        public string TokenKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Lifetime { get; set; }
    }
}
