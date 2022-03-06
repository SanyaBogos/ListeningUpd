using Listening.Server.Entities.Specialized.Result;
using Listening.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listening.Core.Entities.Specialized.Crossword;

namespace Listening.Server.Repositories.Abstract
{
    public interface ICrossWordRepository : IRepositoryPostgres<Crossword, long>
    {
        // Task<IEnumerable<Crossword>> GetCrosswordsList();
        // Task<IEnumerable<Crossword>> GetCrossword(long id);
    }
}
