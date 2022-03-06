using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.AccountViewModels
{
    public class UserWithRolesMOViewModel
    {
        public UserWithRoleMOViewModel[] Users { get; set; }
        public RoleViewModel[] Roles { get; set; }
    }
}
