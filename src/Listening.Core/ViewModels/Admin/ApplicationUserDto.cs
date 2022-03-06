using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Admin
{
    public class ApplicationUserDto
    {
        public bool IsEnabled { get; set; }
        public long Id { get; set; }
        //public DateTime CreatedDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Passwd { get; set; }
        public string Role { get; set; }
        //public long RoleId { get; set; }
    }
}
