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

namespace Listening.Server.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Moderator,Admin,Super")]
    public class TextController : BaseController
    {
        private readonly ITextService _textService;
        private readonly IFileService _fileService;
        private readonly IResultService _resultService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TextController(
            UserManager<ApplicationUser> userManager,
            ITextService textService,
            IResultService resultService,
            IUserService userService,
            IMapper mapper,
            IFileService fileService) : base(userManager)
        {
            _textService = textService;
            _fileService = fileService;
            _resultService = resultService;
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("queried/")]
        [LogFilter]
        public async Task<PagedDataViewModel<TextDescriptionDto>> GetTexts([FromBody] TextQueryViewModel query)
        {
            var pagedTexts = await _textService.GetPaged(query);

            var result = new PagedDataViewModel<TextDescriptionDto>
            {
                Count = pagedTexts.Count,
                Data = _mapper.Map<Text[], TextDescriptionDto[]>(pagedTexts.Data)
            };

            return result;
        }

        [HttpPost("queriedEnhanced/")]
        public async Task<PagedDataViewModel<TextDescriptionEnhancedDto>> GetTextsEnhanced([FromBody] AdminTextQueryViewModel query)
        {
            var pagedTexts = await _textService.GetPagedEnhanced(query);

            return pagedTexts;
        }

        [HttpGet("{textId}")]
        public async Task<TextWithAdminsListDto> GetText(string textId)
        {
            var textDto = await _textService.GetTextDtoById(textId);
            var currentUserId = (await GetCurrentUserAsync()).Id;
            var admins = await _userService.GetAdmins(currentUserId);
            return new TextWithAdminsListDto { Admins = admins, TextDto = textDto };
        }

        [HttpPost("addText")]
        [InsertLogDataFilter]
        public async Task<StringIdsDto> PostText([FromBody] TextDto textDto)
        {
            CheckTextDtoCorrectness(textDto);

            if (textDto.Assignee == 0)
                textDto.Assignee = (await GetCurrentUserAsync()).Id;

            var textDtoToInsert = new TextDto[] { textDto };

            return new StringIdsDto
            {
                Ids = await _textService.Insert(textDtoToInsert)
            };
        }

        [HttpPut("updateText")]
        [InsertLogDataFilter]
        public async Task<bool> PutText([FromBody] TextDto textDto)
        {
            CheckTextDtoCorrectness(textDto);
            var user = await GetCurrentUserAsync();
            var isAdmin = await IsAdminOrSuperAsync(user);
            var warningExist = await _textService.Update(new TextDto[] { textDto }, user, isAdmin);

            return warningExist;
        }

        [HttpDelete("{id}")]
        public async Task DeleteText(string id)
        {
            var fileDescription = await _textService.GetTextAttachmentName(id);
            await _textService.Delete(id);
            _fileService.DeleteFile(new FileDescription[] { fileDescription });
        }

        [HttpGet("backup")]
        public FileResult GetBackup()
        {
            var allTextsJSON = _textService.GetTexts().ToArray().ToJson();
            var backupPath = _fileService.GetSavedListeningBackupPath(allTextsJSON);
            var result = PhysicalFile(backupPath, "application/zip",
                backupPath.GetFileNameFromPath());
            return result;
        }

        [RequestFormLimits(ValueLengthLimit = 8192, MultipartBodyLengthLimit = 1073741824)]
        [DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        [HttpPost("restoreFromBackup")]
        public async Task RestoreFromBackup(IFormFile file)
        {
            var backupPath = _fileService.SaveBackupFile(file);
            var serializedData = await _fileService.GetExtractedSerializedData(backupPath);

            // TODO: remove
#if DEBUG
            serializedData = serializedData.Replace($@", ""ShouldBeDeleted"" : false", "");
#endif
            var texts = BsonSerializer.Deserialize<Text[]>(serializedData);
            await _textService.Restore(texts);
        }

        [HttpPost("preview/{isHidden}")]
        public TextForGuessingDto GetTextPrview(bool isHidden, [FromBody] string text)
        {
            if (isHidden)
            {
                var wordCounts = _textService.GetWordCountsByText(text);
                return new TextForGuessingDto { WordsCounts = wordCounts };
            }

            return PreviewHinted(text);
        }

#if DEBUG
        [HttpGet("resave")]
        public async Task Resave()
        {
            // await _textService.ResaveAndRecalculateAllTexts();
            await _textService.ResaveAndAddCreatedDateForTexts();
        }
#endif

        private TextForGuessingDto PreviewHinted(string text)
        {
            var wordsInParagraphs = _textService.GetWordsInParagraphsByText(text);
            var wordCounts = _textService.GetWordCountsByText(wordsInParagraphs);
            var resultsPreview = _resultService.BuildPreviewResultString(wordCounts);
            var mergedText = _resultService.MergeEncodingWithText(resultsPreview, wordsInParagraphs);

            var textGuessingDto = new TextForGuessingDto
            {
                ResultsEncodedString = resultsPreview,
                MergedText = mergedText
            };

            return textGuessingDto;
        }

        private void CheckTextDtoCorrectness(TextDto textDto)
        {
            if (string.IsNullOrEmpty(textDto.AudioName) && string.IsNullOrEmpty(textDto.VideoName))
                throw new TextException(GlobalConstats.AUDIO_OR_VIDEO_IS_NECESSARY);

            if (!string.IsNullOrEmpty(textDto.AudioName) && !string.IsNullOrEmpty(textDto.VideoName))
                throw new TextException(GlobalConstats.NOT_ALLOWED_TO_USE_BOTH_AUDIO_AN_VIDEO);
        }
    }
}
