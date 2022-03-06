using Listening.Core.Entities.Specialized.Knowledge;
using Listening.Core.ViewModels.Spec;
using Listening.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Postgres
{
    public class TimeStampEFRepository : /*EntityBaseRepository<TimeStamp>,*/ ITimeStampRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TimeStamp> _dbset;
        //private readonly DbSet<TimeStamp> _dbset;

        public TimeStampEFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = context.Set<TimeStamp>();
        }

        public async Task<TimeStampUserDto[]> GetAsync(int videoId)
        {
            var result = await _dbset.Where(x => x.VideoId == videoId)
                //.Include(x => x.Video)
                .Include(x => x.User)
                .OrderBy(x => x.UserId).ThenBy(x => x.VideoId)
                .ThenBy(x => x.Seconds)
                .Select(x => new TimeStampUserDto
                {
                    UserId = x.UserId,
                    VideoId = x.VideoId,
                    Seconds = x.Seconds,
                    Comment = x.Comment,
                    UserEmail = x.User.Email
                })
                .ToArrayAsync();
            return result;
        }

        public async Task AddNowAsync(TimeStamp timeStamp)
        {
            _dbset.Add(timeStamp);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNowAsync(TimeStamp timeStamp)
        {
            var ts = _dbset.FirstOrDefault(x => x.UserId == timeStamp.UserId
                                        && x.VideoId == timeStamp.VideoId && x.Seconds == timeStamp.Seconds);

            if (ts == null)
                return;

            ts.Comment = timeStamp.Comment;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteForVideoNowAsync(int videoId)
        {
            var toRemove = _dbset.Where(x => x.VideoId == videoId);
            _dbset.RemoveRange(toRemove);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNowAsync(TimeStamp timeStamp)
        {
            var ts = _dbset.FirstOrDefault(x => x.UserId == timeStamp.UserId
                                        && x.VideoId == timeStamp.VideoId && x.Seconds == timeStamp.Seconds);

            if (ts == null)
                return;

            _dbset.Remove(ts);
            await _context.SaveChangesAsync();
        }
    }
}
