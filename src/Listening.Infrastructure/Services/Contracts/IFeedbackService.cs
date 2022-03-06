using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IFeedbackService
    {
        Task<PagedData<FeedbackDto>> GetPagedAsync(FeedbackQueryViewModel query, long userId, bool isSuper);
        Task<long> Insert(FeedbackInsertDto feedbackDtos, long userId);
    }
}
