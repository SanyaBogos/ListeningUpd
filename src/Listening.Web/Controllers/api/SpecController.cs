using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Spec;
using Listening.Infrastructure.Services.Contracts;
using Listening.Web.Controllers.api.Custom;
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
    [Authorize(Roles = "Specific, SpecAdm")]
    public class SpecController : BaseController
    {
        private readonly ISpecService _specService;

        public SpecController(UserManager<ApplicationUser> userManager,
            ISpecService specService) : base(userManager)
        {
            _specService = specService;
        }

        [HttpGet("header")]
        public async Task<TypeHeaderDto[]> GetHeaderDescription()
        {
            var user = await GetCurrentUserAsync();
            var header = await _specService.GetHeaderDescription(user.Id);
            return header;
        }

        [HttpGet("video/{id}")]
        public async Task<CourseDto> GetVideoDescriptionList(int id)
        {
            var user = await GetCurrentUserAsync();
            var course = await _specService.GetCourse(id, user.Id);
            return course;
        }

    }
}
