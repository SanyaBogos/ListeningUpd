using Listening.Server.Entities.Specialized;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Filters
{
    public class InsertLogDataFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionConstraint = (HttpMethodActionConstraint)context.ActionDescriptor
                .ActionConstraints.First();
            var type = actionConstraint.HttpMethods.First();
            var arg = context.ActionArguments.First(x => x.Value is LogInfo);
            var logInfo = arg.Value as LogInfo;
            var controller = context.Controller as BaseController;

            if (type.Equals("POST"))
            {
                logInfo.CreatedBy = (await controller.GetCurrentUserAsync()).Id;
                logInfo.CreatedDate = DateTime.Now;
            }
            else if (type.Equals("PUT"))
            {
                logInfo.UpdatedBy = (await controller.GetCurrentUserAsync()).Id;
                logInfo.LastModifiedDate = DateTime.Now;
            }

            await next();
        }
    }
}
