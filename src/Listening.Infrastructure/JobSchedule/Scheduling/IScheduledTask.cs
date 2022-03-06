using System.Threading;
using System.Threading.Tasks;

namespace Listening.JobSchedule
{
    public interface IScheduledTask
    {
        string Schedule { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}