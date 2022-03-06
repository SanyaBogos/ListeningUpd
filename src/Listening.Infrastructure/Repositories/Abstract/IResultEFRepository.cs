using Listening.Server.Entities.Specialized.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IResultEFRepository
    {
        Task<Result[]> TextReferencedByResults(string[] ids);
    }
}
