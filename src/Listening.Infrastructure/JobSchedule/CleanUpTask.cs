using Listening.Core;
using Listening.Server.Services.Contracts;
using Listening.Server.Services.Duplicates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listening.JobSchedule
{
    public class CleanUpTask : IScheduledTask
    {
        private readonly ILogger _logger;
        private readonly IFileServiceDuplicate _fileService;
        private readonly ICleanupService _cleanupService;

        public string Schedule { get; }

        public CleanUpTask(
            IConfiguration configuration,
            //ILoggerFactory logger,
            ILogger<CleanUpTask> logger,
            ICleanupService cleanupService,
            IFileServiceDuplicate fileService)
        {
            Schedule = configuration["JobScheduleCron:UnnecessaryFilesCleanUp"];
            //_logger = logger.CreateLogger<CleanUpTask>();
            _logger = logger;
            _cleanupService = cleanupService;
            _fileService = fileService;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(CleanUpTask)} started at {DateTime.Now}");
            await _cleanupService.RemoveFilesForUnexistedTexts(FileContentType.Audio);
            await _cleanupService.RemoveFilesForUnexistedTexts(FileContentType.Video);
            _fileService.RemoveFilesFromTemp();
            _logger.LogInformation($"{nameof(CleanUpTask)} finished at {DateTime.Now}");
        }
    }
}