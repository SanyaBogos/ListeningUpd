using System.IO;
using Microsoft.Extensions.Configuration;

namespace Listening.Infrastructure.Services
{
    public class BaseFileService
    {   
        protected readonly string _tempPath;

        public BaseFileService(IConfiguration configuration)
        {
            _tempPath = $"{Directory.GetCurrentDirectory()}/bin{configuration["Data:FileStorage:Temp"]}";

            if (!Directory.Exists(_tempPath))
                Directory.CreateDirectory(_tempPath);
        }
    }
}