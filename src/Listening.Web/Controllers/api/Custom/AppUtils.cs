using Listening.Core.Entities.Custom;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Listening.Web.Controllers.api.Custom
{
    public static class AppUtils
    {
        internal static IActionResult SignIn(ApplicationUser user, IList<string> roles)
        {
            var userResult = new { User = new { DisplayName = user.UserName, Roles = roles } };
            return new ObjectResult(userResult);
        }

    }
}