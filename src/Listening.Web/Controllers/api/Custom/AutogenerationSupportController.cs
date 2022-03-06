using Listening.Core.ViewModels.Chat;
using Listening.Infrastructure.AcrossAppConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Controllers.api.Custom
{
    [Route("api/[controller]")]
    [Authorize]
    // This methods are useless, hor I created it only for api typescript generation
    // (to avoid any hand writing)
    public class AutogenerationSupportController
    {
        [HttpGet("support1/")]
        public MessageTransferredDto GetChatSupport()
        {
            return new MessageTransferredDto();
        }

        //[HttpGet("support2/")]
        //public Constants GetConstSupport()
        //{
        //    return new Constants();
        //}
    }
}
