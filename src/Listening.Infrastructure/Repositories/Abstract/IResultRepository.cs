using Listening.Server.Entities.Specialized.Result;
using Listening.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listening.Core.ViewModels.ListeningResult;

namespace Listening.Server.Repositories.Abstract
{
    public interface IResultRepository : IRepositoryPostgres<Result, long>
    {
        Task<Result> GetNonCompletedResult(Result templateResult);
        Task UpdateTimeSpentForNonCompletedResult(ResultUpdateTimeDto result);
        Task<IEnumerable<Result>> GetResults(long user);
        Task<bool> IsReferenceExists(string[] ids);
    }
}
