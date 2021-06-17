using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.BLL.Models
{
    public class ServiceResultStruct<T> where T : struct
    {
        [BindRequired]
        public T Result { get; set; }
        [BindRequired]
        public ServiceResultType ServiceResultType { get; set; }
        public string Message { get; set; }
    }
}
