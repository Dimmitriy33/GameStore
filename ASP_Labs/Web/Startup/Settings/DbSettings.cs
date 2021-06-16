namespace WebApp.Web.Startup.Settings
{
    public class DbSettings
    {
        public string Host { get; set; }
        public string Instance { get; set; }
        public string ConnectionString => $"Server={Host};Database={Instance};integrated security=True;Trusted_Connection=True;MultipleActiveResultSets=true";
    }
}
