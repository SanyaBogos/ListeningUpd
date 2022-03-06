using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Listening.Core.ViewModels.Log;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface ILogService
    {
        string[] GetFilesArray(bool isError);
        Task<LogDto[]> GetLogs(string fileName);
        Task<ErrorLogDto[]> GetErrors(string fileName);
    }
}
