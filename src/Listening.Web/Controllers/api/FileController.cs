using Listening.Core.Entities.Custom;
using Listening.Server.Services.Contracts;
using Listening.Core.ViewModels.File;
using Listening.Core.ViewModels.Text;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using System;
using Listening.Core.Entities.Specialized.ServiceModels;
using Listening.Core;
using Listening.Web.Filters;
using Listening.Core.ViewModels.Steg;
using Listening.Server.Entities.Specialized.ServiceModels;

namespace Listening.Server.Controllers.api
{
    [Authorize(Roles = "Admin,Super")]
    [Route("api/[controller]")]
    public class FileController : BaseController
    {
        private readonly IFileService _fileService;

        public FileController(
            UserManager<ApplicationUser> userManager,
            IFileService fileService) : base(userManager)
        {
            _fileService = fileService;
        }

        [HttpPost("audio/{name}")]
        public FileNameViewModel SaveListeningAudio(string name, IFormFile file)
        {
            return new FileNameViewModel(_fileService.SaveListeningAudioFile(name, file));
        }

        [HttpPost("lstng-vid/{name}")]
        [RequestSizeLimit(333888000)]
        public FileNameViewModel SaveListeningVideo(string name, IFormFile file)
        {
            return new FileNameViewModel(_fileService.SaveListeningVideoFile(name, file));
        }

        [HttpPost("blog-vid/{name}")]
        [RequestSizeLimit(333888000)]
        public FileNameViewModel SaveBlogVideo(string name, IFormFile file)
        {
            return new FileNameViewModel(_fileService.SaveBlogVideoFile(name, file));
        }

        [HttpPost("spec-vid/{name}")]
        [RequestSizeLimit(333888000)]
        public FileNameViewModel SaveSpecVideo(string name, IFormFile file)
        {
            return new FileNameViewModel(_fileService.SaveSpecVideoFile(name, file));
        }

        [HttpPost("stegPic/{name}")]
        public FileNameViewModel SaveStegPicture(string name, IFormFile file)
        {
            var result = new FileNameViewModel(_fileService.SaveStegFile(name, FileContentType.StegPict, file));
            return result;
        }

        [AllowAnonymous]
        [CaptchaFilter]
        [HttpPost("stegPic/{captcha}/{hash}/{name}")]
        public FileNameViewModel SaveStegPictureAno(string captcha, string hash, string name, IFormFile file)
        {
            var result = new FileNameViewModel(_fileService.SaveStegFile(name, FileContentType.StegPict, file));
            return result;
        }

        [AllowAnonymous]
        [CaptchaFilter]
        [HttpPost("stegAud/{captcha}/{hash}")]
        public FileNameViewModel SaveStegAudio(string captcha, string hash, string name, IFormFile file)
        {
            var result = new FileNameViewModel(_fileService.SaveStegFile(name, FileContentType.StegAud, file));
            return result;
        }

        [AllowAnonymous]
        [CaptchaFilter]
        [HttpPost("stegVid/{captcha}/{hash}")]
        public FileNameViewModel SaveStegVideo(string captcha, string hash, string name, IFormFile file)
        {
            var result = new FileNameViewModel(_fileService.SaveStegFile(name, FileContentType.StegVid, file));
            return result;
        }

        [HttpPost("video")]
        public async Task<VideoFileViewModel> GetVideo([FromBody] FileNameViewModel fileName)
        {
            return await _fileService.SaveVideoFile(fileName.Name);
        }

        [HttpDelete("blog-vid/{name}")]
        public void DeleteBlogVideo(string name)
        {
            var fileDescriptions = new FileDescription[] { new FileDescription(name, FileContentType.BlogVid) };
            _fileService.DeleteFile(fileDescriptions);
        }

        [HttpPost("cutVideo")]
        public VideoFileViewModel CutVideo([FromBody] CuttingOptionsViewModel options)
        {
            return new VideoFileViewModel(_fileService.CutVideo(options));
        }

        [Authorize(Roles = "Spec")]
        [HttpGet("specVideo/{type}/{author}/{name}")]
        public ActionResult GetSpecVideo(char type, char author, char innerType, string name)
        {
            var specVideoDescription = new SpecVideoDescription
            {
                Type = type,
                Author = author,
                InnerType = innerType,
                Name = name
            };

            var stream = _fileService.GetSpecVideoStream(specVideoDescription);

            return new FileStreamResult(stream, "video/mp4");
        }


    }
}
