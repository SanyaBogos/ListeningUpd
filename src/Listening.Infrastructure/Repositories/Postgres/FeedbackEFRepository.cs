using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Postgres
{
    public class FeedbackEFRepository : EntityBaseRepository<Feedback>, IFeedbackEFRepository
    {
        private readonly DbSet<Feedback> _dbset;

        public FeedbackEFRepository(ApplicationDbContext context) : base(context)
        {
            _dbset = _context.Set<Feedback>();
        }

        // no changes for now (only paging data (no filtering))
        public async Task<PagedData<FeedbackDto>> GetPagedDataAsync(QueryViewModel query, long userId, bool isSuper)
        {
            var dbSet = _dbset.Include(x => x.User)
                .Where(x => isSuper || x.IsVisible || x.UserId == userId)
                .Select(x => new FeedbackDto
                {
                    CreatedTime = x.CreatedTime,
                    Details = x.Details,
                    Id = x.Id,
                    IsVisible = x.IsVisible,
                    Topic = x.Topic,
                    Email = x.User.Email
                });
            var countTask = dbSet.CountAsync();

            var ordered = dbSet.OrderByDescending(x => x.Id);
            var resultTask = dbSet.Skip((query.Page - 1) * query.ElementsPerPage)
                .Take(query.ElementsPerPage).ToArrayAsync();

            await Task.WhenAll(resultTask, countTask);

            var pagedData = new PagedData<FeedbackDto>
            {
                Count = countTask.Result,
                Data = resultTask.Result
            };

            return pagedData;
        }
    }
}
