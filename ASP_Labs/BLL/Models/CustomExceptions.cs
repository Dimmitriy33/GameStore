using System;

namespace WebApp.BLL.Models
{
    public class CustomExceptions : Exception
    {
        public ServiceResultType ErrorStatus { get; }

        public CustomExceptions() { }

        public CustomExceptions(string message) : base(message) { }

        public CustomExceptions(ServiceResultType errorStatus, string message) : base(message)
        {
            ErrorStatus = errorStatus;
        }
    }
}
