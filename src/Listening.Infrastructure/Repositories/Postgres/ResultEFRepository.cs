using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Entities.Specialized.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Postgres
{
    public class ResultEFRepository : EntityBaseRepository<Result>, IResultEFRepository
    {
        private readonly DbSet<Result> _dbset;

        public ResultEFRepository(ApplicationDbContext context) : base(context)
        {
            _dbset = _context.Set<Result>();
        }

        public async Task<Result[]> TextReferencedByResults(string[] ids)
        {
            var result = await _dbset.Where(x => ids.Contains(x.TextId))
                .Select(x => new Result { TextId = x.TextId, IsStarted = x.IsStarted, IsCompleted = x.IsCompleted })
                .ToArrayAsync();
            return result;
        }
    }
}
