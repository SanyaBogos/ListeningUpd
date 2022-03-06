using Listening.Core.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IUserService
    {
        Task<UserViewModel[]> GetAdmins(long currentUserId);
        Task<UserViewModel[]> GetUsers();
        Task<UserWithRolesViewModel> GetUsersWithRoles();
        Task<UserWithRolesMOViewModel> GetUsersWithRolesMO();
    }
}
