using Listening.Server.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Services.Duplicates
{
    public interface IFileServiceDuplicate : IFileService { }

    public class FileServiceDuplicate : FileService, IFileServiceDuplicate
    {
        public FileServiceDuplicate(
            IConfiguration configuration,
            ILogger<FileService> logger,
            IWebHostEnvironment env) : base(configuration, logger, env)
        {
        }
    }
}
