using Listening.Server.Entities.Specialized.Result;
using Listening.Core.ViewModels.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IGetErrorsForSeparatedByResult
    {
        Task<ErrorForSeparatedDto[]> GetErrors(long resultId);
        Task<IEnumerable<ErrorCount>> ErrorsCount(long[] resultIds);
    }
}
