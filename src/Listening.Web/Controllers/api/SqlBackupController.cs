using Listening.Core.Entities.Custom;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Services.Contracts;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Super")]
    public class SqlBackupController : BaseController
    {
        private readonly IBackupService _backupService;
        private readonly IFileService _fileService;

        public SqlBackupController(
            UserManager<ApplicationUser> userManager,
            IBackupService backupService,
            IFileService fileService
            ) : base(userManager)
        {
            _backupService = backupService;
            _fileService = fileService;
        }

        [HttpGet("backup")]
        public FileResult GetBackup()
        {
            var backupPath = _backupService.GetBackup();
            var result = PhysicalFile(backupPath, "application/sql",
                backupPath.GetFileNameFromPath());
            return result;
        }

        [RequestFormLimits(ValueLengthLimit = 8192, MultipartBodyLengthLimit = 1024 * 1024 * 250)]
        [DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        [HttpPost("restoreFromBackup")]
        public void RestoreFromBackup(IFormFile file)
        {
            var backupPath = _fileService.SaveBackupFile(file);
            _backupService.Restore(backupPath);
        }
    }
}
