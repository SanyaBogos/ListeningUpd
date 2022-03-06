using Listening.Core.ViewModels.Spec;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface ITimeCodeService
    {
        Task<TimeStampUserDto[]> GetByVideoId(int videoId);
        Task AddTimeStamp(TimeStampDto timeStampDto);
        Task UpdateTimeStamp(TimeStampDto timeStampDto);
        Task DeleteTimeStampsOfVideo(int videoId);
        Task DeleteTimeStamp(TimeStampDto timeStampDto);
    }
}
