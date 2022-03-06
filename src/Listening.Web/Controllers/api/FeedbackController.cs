using AutoMapper;
using Listening.Core.Entities.Custom;
using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Listening.Infrastructure.Services.Contracts;
using Listening.Core.ViewModels.Text;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listening.Server.Filters;

namespace Listening.Web.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize]
    public class FeedbackController : BaseController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(
            UserManager<ApplicationUser> userManager,
            IFeedbackService feedbackService
            ) : base(userManager)
        {
            _feedbackService = feedbackService;
        }

        [LogFilter]
        [HttpPost("queried/")]
        public async Task<PagedDataViewModel<FeedbackDto>> GetFeedbacks([FromBody]FeedbackQueryViewModel query)
        {
            var currentUser = await GetCurrentUserAsync();
            var isSuper = await IsSuperAsync(currentUser);
            var pagedTexts = await _feedbackService.GetPagedAsync(query, currentUser.Id, isSuper);

            var result = new PagedDataViewModel<FeedbackDto>
            {
                Count = pagedTexts.Count,
                Data = pagedTexts.Data
            };

            return result;
        }

        [HttpPost]
        public async Task<long> PostFeedbacks([FromBody]FeedbackInsertDto feedbackDto)
        {
            var userId = (await GetCurrentUserAsync()).Id;
            return await _feedbackService.Insert(feedbackDto, userId);
        }
    }
}
