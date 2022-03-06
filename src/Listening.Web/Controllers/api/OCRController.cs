using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.OCR;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Services.Contracts;
using Listening.Web.Controllers.api.Custom;
using Listening.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Super")]
    public class OCRController : BaseController
    {
        private readonly IOpticalCharacterRecognitionService _ocrService;

        public OCRController(
            UserManager<ApplicationUser> userManager,
            IOpticalCharacterRecognitionService ocrService
            ) : base(userManager)
        {
            _ocrService = ocrService;
        }

        [HttpPost("recognize/{languages}")]
        public string GetRecognized(string languages, [FromBody]string base64)
        {
            var result = _ocrService.GetRecognitionResult(base64, languages);
            return result;
        }

        [AllowAnonymous]
        [CaptchaFilter]
        [HttpPost("recognizeImg/{languages}")]
        public string GetRecognizedImage(string languages, [FromBody]ImageParamsViewModel imageParams)
        {
            var result = _ocrService.GetRecognitionResult(imageParams.Base64, languages);
            return result;
        }
    }
}
