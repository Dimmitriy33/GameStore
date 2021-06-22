namespace WebApp.BLL.Models
{
    public class ServiceResultClass<T> : ServiceResult where T : class
    {
        public T Result { get; set; }
    }
}
