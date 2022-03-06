using Dapper;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.AccountViewModels;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Repositories.Postgres;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Postgres
{
    public class UserRepository : BasePostgresRepository<ApplicationUser, long>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<UserViewModel[]> GetAdminsAsync(long currentUserId)
        {
            var query = $@"select * from getAdmins(@currentAdminId)";

            using (var connection = Connection)
            {
                var admins = await connection.QueryAsync<UserViewModel>(query,
                                        new { currentAdminId = currentUserId });
                return admins.ToArray();
            }
        }

        public async Task<UserViewModel[]> GetUsersAsync()
        {
            var query = $@"select ""{nameof(UserViewModel.Id)}"", ""{nameof(UserViewModel.Email)}"" 
                                from public.""AspNetUsers""";

            using (var connection = Connection)
            {
                var users = await connection.QueryAsync<UserViewModel>(query);
                return users.ToArray();
            }
        }

        public async Task<UserWithRolesViewModel> GetUsersWithRolesAsync()
        {
            //var usersQuery = $@"select ""{nameof(UserViewModel.Id)}"", ""{nameof(UserViewModel.Email)}"" 
            //                    from public.""AspNetUsers""";

            var usersWithRoles =
                $@"SELECT U.""{nameof(UserViewModel.Id)}"", U.""{nameof(UserViewModel.Email)}"", UR.""RoleId"", R.""{nameof(RoleViewModel.Name)}"" as ""RoleName"" 
                            FROM public.""AspNetUsers"" as U
                    join public.""AspNetUserRoles"" as UR on U.""{nameof(UserViewModel.Id)}""=UR.""UserId""
		            join public.""AspNetRoles"" as R on R.""{nameof(RoleViewModel.Id)}""=UR.""RoleId"";";

            var rolesQuery = $@"SELECT ""{nameof(RoleViewModel.Id)}"", ""{nameof(RoleViewModel.Name)}""	FROM public.""AspNetRoles"";";

            using (var connection = Connection)
            using (var result = connection.QueryMultiple($"{usersWithRoles} {rolesQuery}"))
            {
                var users = result.Read<UserWithRoleViewModel>().ToArray();
                var roles = (await result.ReadAsync<RoleViewModel>()).ToArray();

                return new UserWithRolesViewModel { Users = users, Roles = roles };
                //var users = await connection.QueryAsync<UserViewModel>(usersQuery);
                //return users.ToArray();
            }
        }

        public async Task<UserWithRolesMOViewModel> GetUsersWithRolesMemoryOptimizedAsync()
        {
            var usersWithRoles =
                $@"SELECT U.""{nameof(UserViewModel.Id)}"", U.""{nameof(UserViewModel.Email)}"", UR.""RoleId"" FROM public.""AspNetUsers"" as U
                    join public.""AspNetUserRoles"" as UR on U.""{nameof(UserViewModel.Id)}""=UR.""UserId"";";

            var rolesQuery = $@"SELECT ""{nameof(RoleViewModel.Id)}"", ""{nameof(RoleViewModel.Name)}""	FROM public.""AspNetRoles"";";

            using (var connection = Connection)
            using (var result = connection.QueryMultiple($"{usersWithRoles} {rolesQuery}"))
            {
                var users = result.Read<UserWithRoleMOViewModel>().ToArray();
                var roles = (await result.ReadAsync<RoleViewModel>()).ToArray();

                return new UserWithRolesMOViewModel { Users = users, Roles = roles };
            }
        }

        public async Task<UserViewModel[]> GetUsersByIdsAsync(long[] ids)
        {
            var query = $@"select ""{nameof(UserViewModel.Id)}"", ""{nameof(UserViewModel.Email)}"" 
                                from public.""AspNetUsers""
                                where ""{nameof(UserViewModel.Id)}"" in ({string.Join(',', ids)})";

            using (var connection = Connection)
            {
                var users = await connection.QueryAsync<UserViewModel>(query);
                return users.ToArray();
            }
        }
    }
}
