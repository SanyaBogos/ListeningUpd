using Listening.Core.Entities.Specialized.Knowledge;
using Listening.Core.ViewModels.Spec;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface ITimeStampRepository
    {
        Task<TimeStampUserDto[]> GetAsync(int videoId);
        Task AddNowAsync(TimeStamp timeStamp);
        Task UpdateNowAsync(TimeStamp timeStamp);
        Task DeleteForVideoNowAsync(int videoId);
        Task DeleteNowAsync(TimeStamp timeStamp);
    }
}
