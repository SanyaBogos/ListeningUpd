using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Listening.Core.Entities.Specialized.Knowledge;
using Microsoft.AspNetCore.Identity;

namespace Listening.Core.Entities.Custom
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationRole : IdentityRole<long>
    {
        // public ApplicationRole()
        // {
        //     Courses = new HashSet<Course>();
        // }

        [StringLength(250)]
        public string Description { get; set; }

        // public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Access> Accesses { get; set; }
    }
}
