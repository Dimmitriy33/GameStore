namespace WebApp.Web.Startup.Settings
{
    public class JwtSettings
    {
        public string TokenKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Lifetime { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
    }
}
