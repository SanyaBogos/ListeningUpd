using Listening.Core.ViewModels.DebianFAI;
using Listening.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.Net.Http.Headers;
using Listening.Web.Filters;
using Listening.Server.Filters;

namespace Listening.Web.Controllers.api
{
    [Route("api/[controller]")]
    public class DebianFAIController
    {
        private readonly IDebianFAIService _debianFAIService;

        public DebianFAIController(IDebianFAIService debianFAIService)
        {
            _debianFAIService = debianFAIService;
        }

        [LogFilter]
        [HttpPost("preseed/")]
        public string GetPreseed([FromBody] PreseedSettingsViewModel settings)
        {
            return _debianFAIService.GetPreseed(settings);
        }

        [HttpGet("defaultSettings/")]
        public PreseedSettingsViewModel GetDefaultSettings()
        {
            return _debianFAIService.GetDefaultSettings();
        }

        [CaptchaFilter]
        [HttpPost("image/")]
        public string GetImage([FromBody] PreseedSettingsViewModel settings)
        {
            return _debianFAIService.GetImage(settings);
        }

        //public IActionResult Download()
        //{
        //    // get the file and convert it into a bytearray
        //    var locatedFile = "";
        //    return new FileContentResult(locatedFile, new
        //        MediaTypeHeaderValue("application/octet"))
        //    {
        //        FileDownloadName = "SomeFileDownloadName.someExtensions"
        //    };
        //}

        [HttpGet("downloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            // var path = @"D:\Projects\My Internal Projects\Listening2018-4\listening2018\src\Listening.Web\wwwroot\audio";
            // var bytes = await File.ReadAllBytesAsync($"{path}/{fileName}");
            var bytes = await _debianFAIService.GetFileBytes(fileName);
            var result = new FileContentResult (bytes, new MediaTypeHeaderValue("application/octet"))
            {
                FileDownloadName = fileName
            };

            return result;
            //return new FileStream(, FileMode.Open, FileAccess.Read);
        }

    }
}
