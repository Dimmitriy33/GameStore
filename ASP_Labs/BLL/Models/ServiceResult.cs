namespace WebApp.BLL.Models
{
    public class ServiceResult
    {
        public ServiceResultType ServiceResultType { get; set; }
        public string Message { get; set; }

        public ServiceResult(string message, ServiceResultType serviceResultType)
        {
            ServiceResultType = serviceResultType;
            Message = message;
        }
        public ServiceResult(ServiceResultType serviceResultType)
        {
            ServiceResultType = serviceResultType;
        }
    }
}
