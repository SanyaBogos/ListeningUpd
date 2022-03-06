using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Listening.Infrastructure.Services.Custom
{
    public interface IApplicationDataService
    {
        Task<object> GetApplicationData(HttpContext context);
    }
}