using Listening.Server.Entities.Specialized.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IErrorsForSeparatedRepository : IRepository<ErrorForSeparated, long>, 
        IGetErrorsForSeparatedByResult
    {
    }
}
