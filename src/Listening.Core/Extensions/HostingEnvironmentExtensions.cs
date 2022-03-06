using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Listening.Core
{
    public static class HostingEnvironmentExtensions
    {
        public static string[] GetTranslationFile(this IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
                return File.ReadAllLines(Path.Combine(hostingEnvironment.ContentRootPath, "translations.csv"));
            else
                return File.ReadAllLines("translations.csv");
        }

    }
}
