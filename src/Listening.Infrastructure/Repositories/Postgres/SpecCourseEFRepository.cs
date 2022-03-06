using Listening.Core.Entities.Specialized.Knowledge;
using Listening.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeType = Listening.Core.Entities.Specialized.Knowledge.Type;

namespace Listening.Infrastructure.Repositories.Postgres
{
    public class SpecCourseEFRepository : ISpecCourseEFRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Course> _dbsetCourse;
        private readonly DbSet<KnowledgeType> _dbsetType;

        public SpecCourseEFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbsetCourse = _context.Set<Course>();
            _dbsetType = _context.Set<KnowledgeType>();
        }

        public async Task<KnowledgeType[]> GetHeaderDescription(long userId)
        {
            // CheckIfAllowed(id, userId);
            var userRoles = _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToArray();
            var userRole = userRoles[0];
            var coursesIds = _context.Accesses.Where(x => x.RoleId == userRole).Select(x => x.CourseId).ToArray();

            var query = _dbsetType.Include(x => x.Courses)
                .ThenInclude(x => x.Author)
                .AsQueryable();

            var result = await query.ToArrayAsync();

            // TODO: it would be nice to refactor this solution to use database query instead of modifying on server side, 
            //      however, for now that's it ...
            foreach (var knowledgeType in result)
                knowledgeType.Courses = knowledgeType.Courses.Where(x => coursesIds.Contains(x.Id)).ToArray();

            var finalResult = result.Where(x => x.Courses.Count > 0).ToArray();

            return finalResult;
        }

        public async Task<Course> GetVideoDescriptions(int id, long userId)
        {
            CheckIfAllowed(id, userId);

            var result = await _dbsetCourse.Where(x => x.Id == id).Include(x => x.Type)
                .Include(x => x.Author).Include(x => x.Books).ThenInclude(x => x.FileType)
                .Include(x => x.Folders).ThenInclude(x => x.Videos).ThenInclude(x => x.VideoType)
                .FirstOrDefaultAsync();

            return result;
        }

        private void CheckIfAllowed(int courseId, long userId)
        {
            var userRoles = _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToArray();
            var userRole = userRoles[0];
            var accessAllowed = _context.Accesses.Any(x => x.CourseId == courseId && x.RoleId == userRole);

            if (!accessAllowed)
                throw new Exception($"Access denied for course id '{courseId}' and user id {userId}");
        }
    }
}
