namespace WebApp.BLL.Models
{
    public class ServiceResultStruct<T> : ServiceResult where T : struct
    {
        public T Result { get; set; }
        public ServiceResultStruct(ServiceResultType serviceResultType) : base(serviceResultType) { }
        public ServiceResultStruct(T result, ServiceResultType serviceResultType) : base(serviceResultType)
        {
            Result = result;
        }
    }
}
