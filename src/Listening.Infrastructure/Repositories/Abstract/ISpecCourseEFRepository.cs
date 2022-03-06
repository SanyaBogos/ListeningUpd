using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KnowledgeType = Listening.Core.Entities.Specialized.Knowledge.Type;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface ISpecCourseEFRepository
    {
        Task<KnowledgeType[]> GetHeaderDescription(long userId);
        Task<Course> GetVideoDescriptions(int id, long userId);
    }
}
