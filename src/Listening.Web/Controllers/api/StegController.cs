using Listening.Core.ViewModels.Steg;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Controllers.api
{
    [Route("api/[controller]")]
    public class StegController : Controller
    {
        private readonly IStegPictureService _stegPictureService;

        public StegController(IStegPictureService stegService)
        {
            _stegPictureService = stegService;
        }

        [HttpPost("inject")]
        public FileResult SimplePictureInject([FromBody] StegSettingsDto settings)
        {
            var steggedPath = _stegPictureService.SimpleInjectMessage(settings);
            var result = PhysicalFile(steggedPath, "image/png",
                steggedPath.GetFileNameFromPath());
            return result;
        }

        [HttpPost("eject")]
        public string SimplePictureEject([FromBody] StegSettingsDto settings)
        {
            var resultMessage = _stegPictureService.SimpleEjectMessage(settings);
            return resultMessage;
        }
    }
}
