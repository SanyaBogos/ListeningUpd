using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Text;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Entities.Specialized.Text;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Services.Contracts
{
    public interface ITextService
    {
        Task Delete(string id);
        Task<int> GetSymbolsCount(string textId);
        Task<TextDto> GetTextDtoById(string textId);
        Task<FileDescription> GetTextAttachmentName(string textId);
        Task<string[][]> GetWordCounts(string textId);
        Task<string[][]> GetWordsInParagraphs(string textId);
        List<CorrectWordLocatorsDto> GetCorrectWordLocators(string[] words, string[][] wordsInParagraphs);
        Task<string[]> Insert(TextDto[] textDto);
        Task ResaveAndRecalculateAllTexts();
        Task ResaveAndAddCreatedDateForTexts();
        Task<bool> Update(TextDto[] textDtos, ApplicationUser user, bool isAdmin);
        Task Restore(Text[] texts);
        Task<int[]> GetParagrphsSymbolsCounts(string textId);
        IQueryable<Text> GetTexts();
        IEnumerable<Text> GetTexts(IEnumerable<ObjectId> ids);
        Task<PagedData<Text>> GetPaged(TextQueryViewModel query);
        string[][] GetWordCountsByText(string text);
        string[][] GetWordCountsByText(string[][] wordsInParagraphs);
        string[][] GetWordsInParagraphsByText(string text);
        Task<PagedDataViewModel<TextDescriptionEnhancedDto>> GetPagedEnhanced(AdminTextQueryViewModel query);
    }
}
