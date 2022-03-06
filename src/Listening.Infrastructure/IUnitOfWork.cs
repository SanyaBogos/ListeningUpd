
using System.Threading.Tasks;

namespace Listening.Infrastructure
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
