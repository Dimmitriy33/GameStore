namespace WebApp.BLL.Models
{
    public class ServiceResult
    {
        public ServiceResultType ServiceResultType { get; set; }
        public string Message { get; set; }
        public ServiceResult() { }
        public ServiceResult(ServiceResultType serviceResultType)
        {
            ServiceResultType = serviceResultType;
        }
        public ServiceResult(string message, ServiceResultType serviceResultType)
        {
            ServiceResultType = serviceResultType;
            Message = message;
        }
    }
    public class ServiceResult<T> : ServiceResult
    {
        public T Result { get; set; }

        public ServiceResult() { }
        public ServiceResult(ServiceResultType serviceResultType) : base(serviceResultType) { }
        public ServiceResult(string message, ServiceResultType serviceResultType) : base(message, serviceResultType) { }
        public ServiceResult(T result, ServiceResultType serviceResultType) : base(serviceResultType)
        {
            Result = result;
        }
        public ServiceResult(T result, string message, ServiceResultType serviceResultType) : base(message, serviceResultType)
        {
            Result = result;
        }

    }
}
