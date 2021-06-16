namespace WebApp.BLL.Models
{
    public class ServiceResult<T> where T : class
    {
        public T Result { get; set; }
        public ServiceResultType ServiceResultType { get; set; }
    }
}
