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
    [Authorize(Roles = "Spec")]
    public class TimeStampVideoContrtoller : BaseController
    {
        private readonly ITimeCodeService _timeCodeService;

        public TimeStampVideoContrtoller(UserManager<ApplicationUser> userManager, ITimeCodeService timeCodeService)
            : base(userManager)
        {
            _timeCodeService = timeCodeService;
        }

        [HttpGet("videoTimeCodes/{videoId}")]
        public async Task<TimeStampUserDto[]> Get(int videoId)
        {
            var result = await _timeCodeService.GetByVideoId(videoId);
            return result;
        }

        [HttpPost]
        public async Task Insert(TimeStampDto timeStampDto)
        {
            await _timeCodeService.AddTimeStamp(timeStampDto);
        }

        [HttpPut]
        public async Task Update(TimeStampDto timeStampDto)
        {
            await _timeCodeService.UpdateTimeStamp(timeStampDto);
        }

        [HttpDelete]
        public async Task DeleteTimeStamp(TimeStampDto timeStampDto)
        {
            await _timeCodeService.DeleteTimeStamp(timeStampDto);
        }

        [HttpDelete("{videoId}")]
        public async Task Insert(int videoId)
        {
            await _timeCodeService.DeleteTimeStampsOfVideo(videoId);
        }
    }
}
