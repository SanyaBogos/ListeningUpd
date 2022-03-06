using Listening.Core;
using System.Threading.Tasks;

namespace Listening.Server.Services.Contracts
{
    public interface ICleanupService
    {
        Task RemoveFilesForUnexistedTexts(FileContentType contentType);
    }
}
