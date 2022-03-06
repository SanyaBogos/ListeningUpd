using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.AccountViewModels
{
    public class UserWithRolesViewModel
    {
        public UserWithRoleViewModel[] Users { get; set; }
        public RoleViewModel[] Roles { get; set; }
    }
}
