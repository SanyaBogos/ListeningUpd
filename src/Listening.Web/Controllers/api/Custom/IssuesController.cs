using Listening.Core.Entities.Custom;
using Listening.Infrastructure.Exceptions;
using Listening.Web.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Controllers.api.Custom
{
    [Route("api/[controller]")]
    public class IssuesController : BaseController
    {
        public IssuesController(UserManager<ApplicationUser> userManager)
            : base(userManager)
        {
        }

        [HttpGet("getKnownError")]
        public void GetKnownError()
        {
            throw new ApiException("Known Exception");
        }

        [HttpGet("getUnknownError")]
        public void GetUnknownError()
        {
            throw new Exception("Unknown Exception");
        }

        [HttpGet("getUnauthorizedAccessError")]
        public void GetUnauthorizedAccessError()
        {
            throw new UnauthorizedAccessException("Unauthorized Access Exception");
        }
    }
}
