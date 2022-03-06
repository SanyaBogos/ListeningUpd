using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IRepositoryPostgres<T, Y> : IRepository<T, Y>
    {
        Task<Y> InsertOne(T item);
    }
}
