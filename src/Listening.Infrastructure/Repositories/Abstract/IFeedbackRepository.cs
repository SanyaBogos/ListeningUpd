using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Listening.Server.Entities.Specialized.ServiceModels;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IFeedbackRepository: IRepositoryPostgres<Feedback, long>
    {
        Task<PagedData<FeedbackDto>> GetPaged(FeedbackQueryViewModel query, long userId, bool isSuper);
    }
}
