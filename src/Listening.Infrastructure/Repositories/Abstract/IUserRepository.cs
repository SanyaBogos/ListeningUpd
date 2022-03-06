using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<UserViewModel[]> GetAdminsAsync(long currentUserId);
        Task<UserViewModel[]> GetUsersAsync();
        Task<UserViewModel[]> GetUsersByIdsAsync(long[] ids);
        Task<UserWithRolesViewModel> GetUsersWithRolesAsync();
        Task<UserWithRolesMOViewModel> GetUsersWithRolesMemoryOptimizedAsync();
    }
}
