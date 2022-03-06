using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IRepository<T, Y>
    {
        Task<PagedData<T>> GetPaged(QueryViewModel query);
        Task<T> GetById(Y id);
        Task Insert(IEnumerable<T> items);
        Task Update(IEnumerable<T> item);
        Task Delete(IEnumerable<Y> itemId);
    }
}
