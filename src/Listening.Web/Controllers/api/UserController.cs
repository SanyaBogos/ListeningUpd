using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.AccountViewModels;
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
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Super")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(UserManager<ApplicationUser> userManager,
            IUserService userService) : base(userManager)
        {
            _userService = userService;
        }

        [HttpGet("admins")]
        public async Task<UserViewModel[]> GetAdmins()
        {
            var currentUserId = (await GetCurrentUserAsync()).Id;
            return await _userService.GetAdmins(currentUserId);
        }
    }
}
