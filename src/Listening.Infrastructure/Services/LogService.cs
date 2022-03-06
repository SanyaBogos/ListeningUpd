using AutoMapper;
using Listening.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Listening.Core.ViewModels.Log;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Listening.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private const string ERROR = "error-";
        private const string LOG = "log-";

        private readonly IMapper _mapper;
        private readonly string _logPath;

        public LogService(
            IConfiguration configuration,
            IMapper mapper
            )
        {
            _logPath = $"{Directory.GetCurrentDirectory()}{configuration["Data:FileStorage:Logs"]}";
            _mapper = mapper;
        }

        public string[] GetFilesArray(bool isError)
        {
            var files = Directory.GetFiles(_logPath).Select(x => x.Split("/").Last());

            var filesFiltered = isError ? files.Where(x => x.StartsWith(ERROR))
                            : files.Where(x => x.StartsWith(LOG));

            return filesFiltered.ToArray();
        }

        public async Task<LogDto[]> GetLogs(string fileName)
        {
            var text = await File.ReadAllTextAsync($"{_logPath}/{fileName}");

            var result = text.Split('\n').Where(x => !string.IsNullOrEmpty(x))
                                .Select(CreateLog).ToArray();

            return result;
        }

        public async Task<ErrorLogDto[]> GetErrors(string fileName)
        {
            var text = await File.ReadAllTextAsync($"{_logPath}/{fileName}");

            var result = text.Split("\n\n").Where(x => !string.IsNullOrEmpty(x))
                                .Select(CreateError).ToArray();

            return result;
        }

        private LogDto CreateLog(string str)
        {
            var parts = str.Split('\t');
            var logDto = new LogDto
            {
                Time = Convert.ToDateTime(parts[0]),
                UserName = parts[1],
                IPAddress = parts[2],
                Path = parts[3],
            };

            return logDto;
        }

        private ErrorLogDto CreateError(string str)
        {
            var parts = str.Split('\t');
            var logDto = new ErrorLogDto
            {
                Time = Convert.ToDateTime(parts[0]),
                UserName = parts[1],
                IPAddress = parts[2],
                Message = parts[3],
            };

            return logDto;
        }
    }
}
