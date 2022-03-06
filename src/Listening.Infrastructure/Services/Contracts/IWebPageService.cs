using System.Collections.Generic;
using Listening.Core.ViewModels.DebianFAI;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IWebPageService
    {
        string GetHtmlByUrl(string urlAddress);
        Dictionary<ArchitectureType, string> GetArhitecturesDictionary(string html);
        string DownloadFile(string link, string pathToFolder);
    }
}