using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using WebApp.BLL.Models;

namespace WebApp.Web.Startup.Configuration
{
    public static class ExceptionHandlerExtensions
    {
        private class ExceptionResponse
        {
            public int Status { get; set; }
            public string Message { get; set; }
        }

        public static void RegisterExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature is not null)
                    {
                        var exceptionResponse = new ExceptionResponse();

                        switch (contextFeature.Error)
                        {
                            case CustomExceptions customExceptions:
                                switch (customExceptions.ErrorStatus)
                                {
                                    case ServiceResultType.Not_Found:
                                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                                        break;
                                    case ServiceResultType.Invalid_Data:
                                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                        break;
                                    case ServiceResultType.Internal_Server_Error:
                                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                        break;
                                }

                                exceptionResponse.Status = (int)customExceptions.ErrorStatus;
                                exceptionResponse.Message = contextFeature.Error.Message;
                                break;
                        }

                        logger.LogError($"Error caught in global handler: ${exceptionResponse.Message}");

                        await context.Response.WriteAsync($@"
                            {{
                                ""errors"": [
                                    ""code"":""API_server_error"",
                                    ""status"": ""{exceptionResponse.Status}"",
                                    ""message"":""{exceptionResponse.Message}""
                                ]
                            }}
                        ");
                    }
                });
            });
        }
    }
}
