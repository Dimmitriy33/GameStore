using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.BLL.Models
{
    public class ServiceResultClass<T> where T : class
    {
        [BindRequired]
        public T Result { get; set; }
        [BindRequired]
        public ServiceResultType ServiceResultType { get; set; }
    }
}
