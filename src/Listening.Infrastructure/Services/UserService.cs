using AutoMapper;
using Listening.Core.Extensions;
using Listening.Core.ViewModels.AccountViewModels;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel[]> GetAdmins(long currentUserId)
        {
            var admins = await _userRepository.GetAdminsAsync(currentUserId);
            var indexToChange = -1;

            for (int i = 0; i < admins.Length; i++)
                if (admins[i].Id == currentUserId)
                    indexToChange = i;

            admins.Swap(0, indexToChange);
            return admins;
        }

        public async Task<UserViewModel[]> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            return users;
        }

        public async Task<UserWithRolesViewModel> GetUsersWithRoles()
        {
            var usersWithRoles = await _userRepository.GetUsersWithRolesAsync();
            return usersWithRoles;
        }

        public async Task<UserWithRolesMOViewModel> GetUsersWithRolesMO()
        {
            var usersWithRoles = await _userRepository.GetUsersWithRolesMemoryOptimizedAsync();
            return usersWithRoles;
        }
    }
}
