using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IFeedbackEFRepository : IEntityBaseRepository<Feedback>
    {
        Task<PagedData<FeedbackDto>> GetPagedDataAsync(QueryViewModel query, long userId, bool isSuper);
    }
}
