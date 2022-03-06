using AutoMapper;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Log;
using Listening.Infrastructure.Services.Contracts;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Controllers.api
{
    [Authorize(Roles = "Super")]
    [Route("api/[controller]")]
    public class LogController : BaseController
    {
        private readonly ILogService _logService;

        public LogController(
            UserManager<ApplicationUser> userManager,
            ILogService logService
            ) : base(userManager)
        {
            _logService = logService;
        }

        [HttpGet("files/{isError}")]
        public string[] GetFilesArray(bool isError)
        {
            var logs = _logService.GetFilesArray(isError);
            return logs;
        }

        [HttpGet("logs/{filename}")]
        public async Task<LogDto[]> GetLogs(string filename)
        {
            var logs = await _logService.GetLogs(filename);
            return logs;
        }

        [HttpGet("errs/{filename}")]
        public async Task<ErrorLogDto[]> GetErrors(string filename)
        {
            var logs = await _logService.GetErrors(filename);
            return logs;
        }
    }
}
