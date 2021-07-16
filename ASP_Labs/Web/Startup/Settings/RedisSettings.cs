using System.ComponentModel.DataAnnotations;

namespace WebApp.Web.Startup.Settings
{
    public class RedisSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public int PoolSize { get; set; }
        public int ConnectionTimeOut { get; set; }
        public string ConnectionString => $"{Host}:{Port},connectTimeout={ConnectionTimeOut}";

        public void Validate()
            => Validator.ValidateObject(this, new ValidationContext(this), true);
    }
}
