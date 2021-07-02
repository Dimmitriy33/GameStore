namespace WebApp.BLL.Models
{
    public class ServiceResultClass<T> : ServiceResult where T : class
    {
        public T Result { get; set; }

        public ServiceResultClass(ServiceResultType serviceResultType) : base(serviceResultType) {}
        public ServiceResultClass(string message, ServiceResultType serviceResultType) : base(message, serviceResultType) {}
        public ServiceResultClass(T result, ServiceResultType serviceResultType) : base(serviceResultType)
        {
            Result = result;
        }
        public ServiceResultClass(T result, string message, ServiceResultType serviceResultType) : base(message, serviceResultType)
        {
            Result = result;
        }

    }
}
