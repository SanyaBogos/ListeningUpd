using Listening.Core.ViewModels.ListeningResult;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listening.Core.ViewModels.CrossWord;
using Listening.Core.Entities.Custom;
using Listening.Core.Entities.Specialized.Crossword;

namespace Listening.Server.Services.Contracts
{
    public interface ICrossWordService
    {
        Task<IEnumerable<CrosswordDto>> GetCrosswordsList();
        Task<CrosswordDto> GetCrossword(long id);
        Task<long[]> Insert(CrosswordDto[] crosswordDtos);
        Task<long[]> Update(CrosswordDto[] crosswordDtos, ApplicationUser user, bool isAdmin);
        Task Restore(Crossword[] texts);
        Task Delete(long id);
    }
}
