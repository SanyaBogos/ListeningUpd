using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Listening.Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Listening.Web.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private const string FILE_NAME = "error";

        private ILogger<ApiExceptionFilter> _logger;
        private IWebHostEnvironment _env;
        private string _logPath;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger, IWebHostEnvironment env, IConfiguration configuration)
        {

            _logger = logger;
            _env = env;
            _logPath = $"{env.ContentRootPath}{configuration["Data:FileStorage:Logs"]}";
        }


        public override void OnException(ExceptionContext context)
        {
            ApiError apiError;
            if (context.Exception is ApiException)
            {
                // handle explicit 'known' API errors
                var ex = context.Exception as ApiException;
                context.Exception = null;
                apiError = new ApiError(ex.Message)
                {
                    Errors = ex.Errors
                };

                context.HttpContext.Response.StatusCode = ex.StatusCode;

                _logger.LogError($"Application thrown error: {ex.Message}", ex);
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;
                _logger.LogError("Unauthorized Access in Controller Filter.");
            }
            else
            {
                // Unhandled errors
                var msg = "";
                var stack = "";
                if (_env.IsDevelopment())
                {
                    msg = context.Exception.GetBaseException().Message;
                    stack = context.Exception.StackTrace;
                }
                else
                {
                    msg = "An unhandled error occurred.";
                    stack = null;
                }

                apiError = new ApiError(msg)
                {
                    detail = stack
                };

                context.HttpContext.Response.StatusCode = 500;

                // handle logging here
                _logger.LogError(new EventId(0), context.Exception, msg);
            }

            // always return a JSON result
            context.Result = new JsonResult(apiError);

            WriteToFile(context);

            base.OnException(context);
        }

        private void WriteToFile(ExceptionContext context)
        {
            var dateTime = DateTime.Now;
            var userName = context.HttpContext.User != null ? context.HttpContext.User.Identity.Name : "";
            var text = $"{dateTime.ToString("HH:mm:ss")}\t{userName}\t{context.HttpContext.Connection.RemoteIpAddress}\t{context.Exception}\n\n";
            File.AppendAllText($"{_logPath}/{FILE_NAME}-{dateTime.ToString("dd-MMM-yyyy")}.txt", text);
        }
    }
}