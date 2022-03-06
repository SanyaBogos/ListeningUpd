using AutoMapper;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.AccountViewModels;
using Listening.Core.ViewModels.Admin;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Services.Contracts;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Super")]
    public class BackupController : BaseController
    {   
        private const string Zip = "application/zip";

        private readonly ITextService _textService;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IBackupService _backupService;
        private readonly IMapper _mapper;

        public BackupController(UserManager<ApplicationUser> userManager,
            IUserService userService,
            ITextService textService,
            IBackupService backupService,
            IMapper mapper,
            IFileService fileService
            ) : base(userManager)
        {
            _textService = textService;
            _fileService = fileService;
            _userService = userService;
            _backupService = backupService;
            _mapper = mapper;
        }


        [HttpGet("full-bckp")]
        public FileResult GetFullBackup()
        {
            var allTextsJSON = _textService.GetTexts().ToArray().ToJson();
            var backupPath = _fileService.GetSavedFullBackupPath(allTextsJSON);
            var result = PhysicalFile(backupPath, Zip, backupPath.GetFileNameFromPath());
            return result;
        }

        [HttpGet("sql-bckp")]
        public FileResult GetSQLBackup()
        {
            var backupPath = _backupService.GetBackup();
            var result = PhysicalFile(backupPath, Zip, backupPath.GetFileNameFromPath());
            return result;
        }

        [HttpGet("spec-bckp")]
        public FileResult GetSpecBackup()
        {
            var backupPath = _fileService.GetSavedSpecFilesBackupPath();
            var result = PhysicalFile(backupPath, Zip, backupPath.GetFileNameFromPath());
            return result;
        }

        [HttpGet("blog-bckp")]
        public FileResult GetBlogBackup()
        {
            var backupPath = _fileService.GetSavedBlogFilesBackupPath();
            var result = PhysicalFile(backupPath, Zip, backupPath.GetFileNameFromPath());
            return result;
        }

        [RequestFormLimits(ValueLengthLimit = 8192, MultipartBodyLengthLimit = 10073741824)]
        [DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        [HttpPost("rstr-blog")]
        public async Task RestoreBlogData(IFormFile file)
        {
            var backupPath = _fileService.SaveBackupFile(file);
            await _fileService.RestoreBlogData(backupPath);
        }

        [RequestFormLimits(ValueLengthLimit = 8192, MultipartBodyLengthLimit = 10073741824)]
        [DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        [HttpPost("rstr-spec")]
        public async Task RestoreSpecData(IFormFile file)
        {
            var backupPath = _fileService.SaveBackupFile(file);
            await _fileService.RestoreSpecData(backupPath);
        }
    }
}
