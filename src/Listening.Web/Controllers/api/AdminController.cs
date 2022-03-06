using AutoMapper;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.AccountViewModels;
using Listening.Core.ViewModels.Admin;
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
    [Authorize(Roles = "Super")]
    public class AdminController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(UserManager<ApplicationUser> userManager,
            IUserService userService,
            IMapper mapper
            ) : base(userManager)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("ur")]
        public async Task<UserWithRolesViewModel> GetUsersAndRoles()
        {
            var usersWithRoles = await _userService.GetUsersWithRoles();
            return usersWithRoles;
        }

        [HttpGet("urmo")]
        public async Task<UserWithRolesMOViewModel> GetUsersAndRolesMO()
        {
            var usersWithRoles = await _userService.GetUsersWithRolesMO();
            return usersWithRoles;
        }

        [HttpGet("user/{id}")]
        public async Task<ApplicationUserDto> GetUser(long id)
        {
            var user = await _userManager.FindByIdAsync($"{id}");
            var roles = await _userManager.GetRolesAsync(user);
            
            var userDto = _mapper.Map<ApplicationUserDto>(user);
            userDto.Role = roles.FirstOrDefault();
            return userDto;
        }

        [HttpPost]
        public async Task<long> AddUser([FromBody] ApplicationUserDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            await _userManager.CreateAsync(user, userDto.Passwd);
            var userInDb = await _userManager.FindByNameAsync(user.UserName);
            (await _userManager.AddToRoleAsync(userInDb, userDto.Role)).ToString();

            return userInDb.Id;
        }

        [HttpPut]
        public async Task UpdateUser([FromBody] ApplicationUserDto userDto)
        {
            var userToSave = _mapper.Map<ApplicationUser>(userDto);
            var user = await _userManager.FindByIdAsync($"{userDto.Id}");

            user.FirstName = userToSave.FirstName;
            user.LastName = userToSave.LastName;
            user.Email = userToSave.Email;
            user.EmailConfirmed = userToSave.EmailConfirmed;
            user.IsEnabled = userToSave.IsEnabled;

            await _userManager.UpdateAsync(user);

            //var user = await _userManager.FindByEmailAsync(userDto.Email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, userDto.Passwd);

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (!currentRoles.Contains(userDto.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, userDto.Role);
            }
        }
    }
}
