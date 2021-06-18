namespace WebApp.Web.Startup.Settings
{
    public class DbSettings
    {
        public string Host { get; set; }
        public string Instance { get; set; }
        public string Username { get; set; }
        public string Password_hash { get; set; }
        public string ConnectionString => $"Server={Host};Database={Instance};User={Username};Password={Password_hash};integrated security=True;Trusted_Connection=True;MultipleActiveResultSets=true";
    }
}
