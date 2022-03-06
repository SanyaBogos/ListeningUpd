using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.ListeningResult;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Services.Contracts;
using Listening.Core.ViewModels.Text;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Listening.Server.Filters;

namespace Listening.Server.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize]
    public class ResultController : BaseController
    {
        private readonly IResultService _resultService;
        private readonly ITextService _textService;

        public ResultController(
            UserManager<ApplicationUser> userManager,
            IResultService resultService,
            ITextService textService
            ) : base(userManager)
        {
            _resultService = resultService;
            _textService = textService;
        }

        [HttpGet("allUserTextResults/{userId}")]
        [LogFilter]
        public async Task<ResultEnhancedDto> GetAllUserTextResults(long userId)
        {
            var user = await GetCurrentUserAsync();
            long userIdToSearch;

            if (await IsAdminOrSuperAsync(user) && userId > 0)
                userIdToSearch = userId;
            else
                userIdToSearch = user.Id;

            var results = await _resultService.GetResultsForUser(userIdToSearch);
            return results;
        }

        [HttpGet("detailedResult/{id}")]
        public async Task<TextForGuessingDto> GetDetailedResult(long id)
        {
            var user = await GetCurrentUserAsync();
            var result = new Result { UserId = user.Id, Id = id };
            var isAdminOrSuper = await IsAdminOrSuperAsync(user);

            var resultFromDB = await _resultService.GetTextGuessedResult(result, isAdminOrSuper);
            var mergedResult = await _resultService.GetMergedEncodingWithText(resultFromDB);

            return mergedResult;
        }

        [HttpPut("updateLeaveTime")]
        public async Task UpdateLeaveTime([FromBody]ResultUpdateTimeDto result)
        {
            var user = await GetCurrentUserAsync();
            result.UserId = user.Id;

            await _resultService.UpdateTimeSpent(result);
        }
    }
}
