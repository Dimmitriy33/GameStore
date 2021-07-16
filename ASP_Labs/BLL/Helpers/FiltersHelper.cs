using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.BLL.Helpers
{
    public class FiltersHelper
    {
        public static void SendBadRequest(ActionExecutingContext context, string message)
            => context.Result = new BadRequestObjectResult(GetErrorMessage(message));

        public static string GetErrorMessage(string parameter)
            => $"Invalid value of {parameter}";
    }
}
