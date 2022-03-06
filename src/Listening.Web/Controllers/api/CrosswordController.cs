using AutoMapper;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Text;
using Listening.Infrastructure.Exceptions;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Entities.Specialized.Text;
using Listening.Server.Filters;
using Listening.Server.Services.Contracts;
using Listening.Core.ViewModels;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Listening.Core.ViewModels.CrossWord;

namespace Listening.Server.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Moderator,Admin,Super")]
    public class CrosswordController : BaseController
    {
        // private readonly ITextService _textService;
        // private readonly IFileService _fileService;
        // private readonly IResultService _resultService;
        // private readonly IUserService _userService;
        // private readonly IMapper _mapper;

        public CrosswordController(
            UserManager<ApplicationUser> userManager
            ) : base(userManager)
        {
        }

        [AllowAnonymous]
        [HttpPost("queried/")]
        public async Task<PagedDataViewModel<CrosswordDescriptionDto>> GetCrosswords([FromBody] CrosswordQueryViewModel query)
        {
            // var pagedTexts = await _textService.GetPaged(query);

            // var result = new PagedDataViewModel<TextDescriptionDto>
            // {
            //     Count = pagedTexts.Count,
            //     Data = _mapper.Map<Text[], TextDescriptionDto[]>(pagedTexts.Data)
            // };

            // return result;

            return null;
        }

        [HttpPost("queriedEnhanced/")]
        public async Task<PagedDataViewModel<CrosswordDescriptionEnhancedDto>> GetTextsEnhanced([FromBody] AdminCrosswordQueryViewModel query)
        {
            throw new System.Exception("Not implemented");
            // var pagedTexts = await _textService.GetPagedEnhanced(query);

            // return pagedTexts;
        }

        [HttpGet("{id}")]
        public async Task<CrosswordWithAdminsListDto> GetCrossword(long id)
        {
            throw new System.Exception("Not implemented");
            // var textDto = await _textService.GetTextDtoById(textId);
            // var currentUserId = (await GetCurrentUserAsync()).Id;
            // var admins = await _userService.GetAdmins(currentUserId);
            // return new TextWithAdminsListDto { Admins = admins, TextDto = textDto };
        }

        [HttpPost("add")]
        [InsertLogDataFilter]
        public async Task<LongIdsDto> Add([FromBody] CrosswordDto crosswordDto)
        {
            CheckCrosswordDtoCorrectness(crosswordDto);

            if (crosswordDto.AssigneeId == 0)
                crosswordDto.AssigneeId = (await GetCurrentUserAsync()).Id;

            throw new System.Exception("Not implemented");

            // var textDtoToInsert = new TextDto[] { textDto };

            // return new LongIdsDto
            // {
            //     Ids = await _textService.Insert(textDtoToInsert)
            // };
        }

        [HttpPut("update")]
        [InsertLogDataFilter]
        public async Task<bool> Update([FromBody] CrosswordDto textDto)
        {
            throw new System.Exception("Not implemented");
            // CheckCrosswordDtoCorrectness(textDto);
            // var user = await GetCurrentUserAsync();
            // var isAdmin = await IsAdminOrSuperAsync(user);
            // var warningExist = await _textService.Update(new TextDto[] { textDto }, user, isAdmin);

            // return warningExist;
        }

        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            throw new System.Exception("Not implemented");

        }

        [HttpGet("backup")]
        public FileResult GetBackup()
        {
            throw new System.Exception("Not implemented");
        }

        private void CheckCrosswordDtoCorrectness(CrosswordDto textDto)
        {
            // if (string.IsNullOrEmpty(textDto.AudioName) && string.IsNullOrEmpty(textDto.VideoName))
            //     throw new CrosswordException(GlobalConstats.AUDIO_OR_VIDEO_IS_NECESSARY);

            // if (!string.IsNullOrEmpty(textDto.AudioName) && !string.IsNullOrEmpty(textDto.VideoName))
            //     throw new CrosswordException(GlobalConstats.NOT_ALLOWED_TO_USE_BOTH_AUDIO_AN_VIDEO);
        }
    }
}
