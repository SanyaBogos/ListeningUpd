using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IRepositoryMongo<T, Y> : IRepository<T, Y>
    {
        IQueryable<T> Get();
        Task UpdateBackup(IEnumerable<T> items);
    }
}
