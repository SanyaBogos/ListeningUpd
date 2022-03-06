using Listening.Server.Entities.Specialized;
using Listening.Web;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        private const string FILE_NAME = "log";

        private readonly string _logPath;

        public LogFilter()
        {
            _logPath = $"{Directory.GetCurrentDirectory()}{Startup.Configuration["Data:FileStorage:Logs"]}";
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            WriteToFile(context);
        }

        private void WriteToFile(ActionExecutedContext context)
        {
            var dateTime = DateTime.Now;
            var userName = context.HttpContext.User != null ? context.HttpContext.User.Identity.Name : "";
            var text = $"{dateTime.ToString("HH:mm:ss")}\t{userName}\t{context.HttpContext.Connection.LocalIpAddress}\t{context.HttpContext.Request.Path}\n\n";
            File.AppendAllText($"{_logPath}/{FILE_NAME}-{dateTime.ToString("dd-MMM-yyyy")}.txt", text);
        }
    }
}
