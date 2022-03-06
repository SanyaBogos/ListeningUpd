using Listening.Core.Entities.Custom;
using Listening.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Listening.Server;

namespace Listening.Web.Controllers.api.Custom
{
    //[Authorize]
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> _userManager;

        public BaseController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected internal async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);

        protected internal async Task<IList<string>> GetCurrentUserRolesAsync(ApplicationUser user) => await _userManager.GetRolesAsync(user);

        protected internal async Task<bool> IsAdminOrSuperAsync(ApplicationUser user)
        {
            var roles = new string[] { GlobalConstats.ADMIN, GlobalConstats.SUPER };
            return (await _userManager.GetRolesAsync(user)).Intersect(roles).Count() >= 1;
        }

        protected internal async Task<bool> IsAdminAsync(ApplicationUser user)
        {
            return (await _userManager.GetRolesAsync(user)).Contains(GlobalConstats.ADMIN);
        }

        protected internal async Task<bool> IsSuperAsync(ApplicationUser user)
        {
            return (await _userManager.GetRolesAsync(user)).Contains(GlobalConstats.SUPER);
        }
    }
}
