using AutoMapper;
using Listening.Core;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Text;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Entities.Specialized.Text;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Services.Contracts;
using Listening.Server.Utilities;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listening.Server.Repositories.Abstract;
using Listening.Server.Entities.Specialized.Result;

namespace Listening.Server.Services
{
    public class TextService : ITextService
    {
        private readonly ITextsMongoRepository _textRepository;
        private readonly IResultEFRepository _resultRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGlobalCache<TextEnhanced, string> _textCache;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public TextService(
            UserManager<ApplicationUser> userManager,
            ITextsMongoRepository textRepository,
            IResultEFRepository resultRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IGlobalCache<TextEnhanced, string> textCache)
        {
            _userManager = userManager;
            _textRepository = textRepository;
            _resultRepository = resultRepository;
            _userRepository = userRepository;
            _textCache = textCache;
            _textCache.UseCache = true;
            _mapper = mapper;
        }

        public async Task<PagedData<Text>> GetPaged(TextQueryViewModel query)
        {
            if (query.FilteringProperties == null)
                query.FilteringProperties = new Dictionary<string, string>();

            return await _textRepository.GetPaged(query);
        }

        public async Task<PagedDataViewModel<TextDescriptionEnhancedDto>> GetPagedEnhanced(AdminTextQueryViewModel query)
        {
            if (query.FilteringProperties == null)
                query.FilteringProperties = new Dictionary<string, string>();

            var resultText = await _textRepository.GetPaged(query);
            var mappedText = _mapper.Map<Text[], TextDescriptionEnhancedDto[]>(resultText.Data);
            var result = new PagedDataViewModel<TextDescriptionEnhancedDto>
            {
                Count = resultText.Count,
                Data = mappedText
            };

            if (!query.IncludeUpdateDate)
                foreach (var data in result.Data)
                    data.LastModifiedDate = null;

            if (!query.IncludeCreateDate)
                foreach (var data in result.Data)
                    data.CreatedDate = null;

            if (!query.IncludeAssignee)
            {
                foreach (var data in result.Data)
                    data.Assignee = null;

                return result;
            }

            var assigneeIds = resultText.Data.Select(x => x.Assignee).ToArray();

            if (assigneeIds == null || assigneeIds.Length == 0)
                return result;

            var assignees = await _userRepository.GetUsersByIdsAsync(assigneeIds);
            var assigneesDisc = assignees.ToDictionary(x => x.Id, y => y.Email);

            for (int i = 0; i < result.Data.Length; i++)
                if (resultText.Data[i] != null && assigneesDisc.ContainsKey(resultText.Data[i].Assignee))
                    result.Data[i].Assignee = assigneesDisc[resultText.Data[i].Assignee].Split('@').First();
                else
                    result.Data[i].Assignee = null;

            return result;
        }

        public async Task ResaveAndAddCreatedDateForTexts()
        {
            var texts = _textRepository.Get().ToArray();

            if (texts == null || !texts.Any())
                return;

            foreach (var text in texts)
                if (text.CreatedDate == null || !text.CreatedDate.HasValue || text.CreatedDate.Value == DateTime.MinValue)
                    text.CreatedDate = new DateTime(2015, 01, 01);

            await _textRepository.UpdateBackup(texts);
        }

        public async Task ResaveAndRecalculateAllTexts()
        {
            var texts = _textRepository.Get().ToArray();

            if (texts == null || !texts.Any())
                return;

            foreach (var text in texts)
            {
                if (text.Country == null)
                    text.Country = "US";

                text.IsDeleted = false;

                // to unset property please run in client mongo the following
                // db.Texts.update({}, {$unset: {ShouldBeDeleted:1}}, false, true);

                text.SymbolsCount = TextTransform.GetSymbolCount(
                    TextTransform.GetWordCounts(text.WordsInParagraphs));

                var newDate = new DateTime();
                var isEmpty = text.CreatedDate == null && text.LastModifiedDate != null;
                var isDefaultDateValue = text.CreatedDate == newDate && text.LastModifiedDate == newDate;

                if (isEmpty || isDefaultDateValue)
                    text.CreatedDate = GlobalConstats.DefaultDate;

                if (text.CreatedBy.HasValue || text.UpdatedBy.HasValue)
                    continue;

                var firstAdmin = Security.SecurityRulesSingleton.Instance.Rules.Users.First(x => x.Role == GlobalConstats.ADMIN);
                var firstAdminUser = await _userManager.FindByEmailAsync(firstAdmin.Email);

                text.CreatedBy = firstAdminUser.Id;
                text.CreatedDate = DateTime.Now;
                text.Assignee = firstAdminUser.Id;
            }

            await _textRepository.Update(texts);
        }

        public IQueryable<Text> GetTexts()
        {
            return _textRepository.Get();
        }

        public IEnumerable<Text> GetTexts(IEnumerable<ObjectId> ids)
        {
            var texts = _textRepository.Get().Where(x => ids.Contains(x.Id)).ToArray();
            return texts;
        }

        // TODO: looks similar with the next one, perhaps should be refactored
        public async Task<string[][]> GetWordCounts(string textId)
        {
            var cachedTextDto = _textCache.GetCached(textId);
            if (cachedTextDto != null)
                return cachedTextDto.CountsInParagraphs;

            if (_textCache.UseCache)
                await ReadAndInsertToCacheText(textId);
            else
            {
                var wordCounts = TextTransform.GetWordCounts(
                    (await _textRepository.GetById(ObjectId.Parse(textId)))
                        .WordsInParagraphs);

                return wordCounts;
            }

            return await GetWordCounts(textId);
        }

        public string[][] GetWordCountsByText(string text)
        {
            var wordsInParagraphs = TextTransform.GetWordInParagraphsByText(text)
                                        .ToArray();
            return GetWordCountsByText(wordsInParagraphs);
        }

        public string[][] GetWordCountsByText(string[][] wordsInParagraphs)
        {
            var wordCounts = TextTransform.GetWordCounts(wordsInParagraphs);
            return wordCounts;
        }

        public string[][] GetWordsInParagraphsByText(string text)
        {
            var wordsInParagraphs = TextTransform.GetWordInParagraphsByText(text)
                                        .ToArray();

            return wordsInParagraphs;
        }

        public async Task<string[][]> GetWordsInParagraphs(string textId)
        {
            var cachedTextDto = _textCache.GetCached(textId);
            if (cachedTextDto != null)
                return cachedTextDto.WordsInParagraphs;

            if (_textCache.UseCache)
                await ReadAndInsertToCacheText(textId);
            else
                return (await _textRepository.GetById(ObjectId.Parse(textId)))
                    .WordsInParagraphs;

            return await GetWordsInParagraphs(textId);
        }

        public List<CorrectWordLocatorsDto> GetCorrectWordLocators(string[] words, string[][] wordsInParagraphs)
        {
            var correctWordLocatorsDtoList = new List<CorrectWordLocatorsDto>();

            foreach (var word in words)
            {
                var locators = new List<WordLocatorDto>();

                for (int i = 0; i < wordsInParagraphs.Length; i++)
                    for (int j = 0; j < wordsInParagraphs[i].Length; j++)
                        if (word.Equals(wordsInParagraphs[i][j], StringComparison.CurrentCultureIgnoreCase))
                            locators.Add(new WordLocatorDto
                            {
                                ParagraphIndex = i,
                                WordIndex = j,
                                IsCapital = char.IsUpper(wordsInParagraphs[i][j].First())
                            });

                if (locators.Count > 0)
                    correctWordLocatorsDtoList.Add(
                        new CorrectWordLocatorsDto { Locators = locators.ToArray(), Word = word });
            }

            return correctWordLocatorsDtoList;
        }

        public async Task<int[]> GetParagrphsSymbolsCounts(string textId)
        {
            var cachedTextDto = _textCache.GetCached(textId);
            if (cachedTextDto != null)
                return cachedTextDto.ParagrphsSymbolsCounts;

            if (_textCache.UseCache)
                await ReadAndInsertToCacheText(textId);
            else
                return TextTransform.GetParagrphsSymbolsCounts(
                    (await _textRepository.GetById(ObjectId.Parse(textId)))
                        .WordsInParagraphs);

            return await GetParagrphsSymbolsCounts(textId);
        }

        public async Task<int> GetSymbolsCount(string textId)
        {
            var cachedTextDto = _textCache.GetCached(textId);
            if (cachedTextDto != null)
                return cachedTextDto.SymbolsCount;

            if (_textCache.UseCache)
                await ReadAndInsertToCacheText(textId);
            else
                return (await _textRepository.GetById(ObjectId.Parse(textId)))
                    .SymbolsCount;

            return await GetSymbolsCount(textId);
        }

        public async Task<TextDto> GetTextDtoById(string textId)
        {
            var cachedTextDto = _textCache.GetCached(textId);

            if (cachedTextDto != null)
                return cachedTextDto;

            return await ReadAndInsertToCacheText(textId);
        }

        public async Task<string[]> Insert(TextDto[] textDtos)
        {
            Text[] texts = textDtos.Select(x => GenerateTextByDto(x)).ToArray();
            await _textRepository.Insert(texts);

            for (int i = 0; i < texts.Length; i++)
                InsertToCache(texts[i], textDtos[i].Text);

            return texts.Select(x => x.Id.ToString()).ToArray();
        }

        public async Task<bool> Update(TextDto[] textDtos, ApplicationUser user, bool isAdmin)
        {
            Text[] textsFromRepo = (await _textRepository.GetAndCheckUpdatePossiblity(textDtos, user, isAdmin)).ToArray();
            Text[] textsRegenerated = textDtos.Select(x => GenerateTextByDto(x)).ToArray();
            Result[] results = await _resultRepository.TextReferencedByResults(textDtos.Select(x => x.Id).ToArray());

            bool noWordInParagraphsChanges = IsWordInParagraphChanged(textsFromRepo, textsRegenerated);
            bool canSubstitute = !results.Any(x => x.IsStarted && !x.Finished.HasValue);
            bool canUpdate = noWordInParagraphsChanges || canSubstitute;
            bool warningExist = false;


            foreach (var textRegen in textsRegenerated)
            {
                if (textRegen.Assignee == 0)
                    textRegen.Assignee = user.Id;

                textRegen.UpdatedBy = user.Id;
                textRegen.LastModifiedDate = DateTime.Now;
            }


            if (canUpdate)
                await _textRepository.Update(textsRegenerated);
            else
            {
                foreach (var text in textsFromRepo)
                    text.IsDeleted = true;

                await _textRepository.Update(textsFromRepo);

                foreach (var text in textsRegenerated)
                    text.Id = ObjectId.Empty;

                warningExist = true;
                await _textRepository.Insert(textsRegenerated);
            }

            for (int i = 0; i < textsRegenerated.Length; i++)
            {
                _textCache.Delete(textDtos[i].Id);
                InsertToCache(textsRegenerated[i], textDtos[i].Text);
            }

            return warningExist;
        }



        public async Task Restore(Text[] texts)
        {
            var existTexts = _textRepository.Get().ToArray();
            var existIds = existTexts.Select(x => x.Id).ToArray();
            var textsToInsert = texts.Where(x => !existIds.Contains(x.Id)).ToArray();
            var textsToUpdate = texts.Where(x => existIds.Contains(x.Id)).ToArray();

            await _textRepository.Insert(textsToInsert);
            await _textRepository.Update(textsToUpdate);
            _textCache.DeleteAll();
        }

        public async Task Delete(string id)
        {
            var toRemove = new ObjectId[] { ObjectId.Parse(id) };
            Result[] results = await _resultRepository.TextReferencedByResults(new string[] { id });

            if (!results.Any())
                await _textRepository.Delete(toRemove);
            else
                await _textRepository.MarkAsDeleted(toRemove);

            _textCache.Delete(id);
        }

        public async Task<FileDescription> GetTextAttachmentName(string textId)
        {
            var cachedTextDto = _textCache.GetCached(textId);
            if (cachedTextDto != null)
                return GetFileDescription(cachedTextDto);

            var text = await _textRepository.GetById(ObjectId.Parse(textId));
            return GetFileDescription(text);
        }

        private FileDescription GetFileDescription(Text text)
        {
            if (!string.IsNullOrEmpty(text.AudioName))
                return new FileDescription(text.AudioName, FileContentType.Audio);

            return new FileDescription(text.VideoName, FileContentType.Video);
        }

        private FileDescription GetFileDescription(TextEnhanced textEnhanced)
        {
            return GetFileDescription(_mapper.Map<Text>(textEnhanced));
        }

        private async Task<TextDto> ReadAndInsertToCacheText(string textId)
        {
            var text = await _textRepository.GetById(ObjectId.Parse(textId));
            return InsertToCache(text);
        }

        private TextEnhanced InsertToCache(Text text, string txt = "")
        {
            var textCache = _mapper.Map<TextEnhanced>(text);
            textCache.Text = string.IsNullOrEmpty(txt)
                ? TextTransform.GetTextFromArray(text.WordsInParagraphs).ToString()
                : txt;
            textCache.CountsInParagraphs =
                TextTransform.GetWordCounts(text.WordsInParagraphs);
            textCache.ParagrphsSymbolsCounts =
                TextTransform.GetParagrphsSymbolsCounts(text.WordsInParagraphs);
            textCache.SymbolsCount = TextTransform.GetSymbolCount(
                TextTransform.GetWordCounts(text.WordsInParagraphs));

            _textCache.Insert(textCache);
            return textCache;
        }

        private Text GenerateTextByDto(TextDto textDto)
        {
            var wordsInParagraphs = TextTransform.GetWordInParagraphsByText(textDto.Text);
            var text = _mapper.Map<Text>(textDto);

            text.WordsInParagraphs = wordsInParagraphs.ToArray();
            text.SymbolsCount = TextTransform.GetSymbolCount(text.WordsInParagraphs);

            return text;
        }

        private bool IsWordInParagraphChanged(Text[] textsFromRepo, Text[] textsRegenerated)
        {
            var noWordInParagraphsChanges = true;

            for (int i = 0; i < textsFromRepo.Length; i++)
            {
                for (int j = 0; j < textsFromRepo[i].WordsInParagraphs.Length; j++)
                {
                    noWordInParagraphsChanges &=
                        Enumerable.SequenceEqual(textsFromRepo[i].WordsInParagraphs[j], textsRegenerated[i].WordsInParagraphs[j]);

                    if (!noWordInParagraphsChanges)
                        return noWordInParagraphsChanges;
                }
            }

            return noWordInParagraphsChanges;
        }
    }
}
