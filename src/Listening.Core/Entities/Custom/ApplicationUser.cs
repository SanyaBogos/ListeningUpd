using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Listening.Server.Entities.Specialized.Result;

namespace Listening.Core.Entities.Custom
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<long>
    {
        public bool IsEnabled { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [StringLength(250)]
        public string FirstName { get; set; }

        [StringLength(250)]
        public string LastName { get; set; }

        [Phone]
        public string Mobile { get; set; }

        [StringLength(22)]
        public string SignalRId { get; set; }

        public int? AppId { get; set; }

        public long? ApplicationUserPhotoId { get; set; }
        public virtual ApplicationUserPhoto ProfilePhoto { get; set; }

        //public int LanguageId { get; set; }
        //public virtual Language Language { get; set; }

        [NotMapped]
        public string Name { get { return $"{this.FirstName} {this.LastName}"; } }

        public virtual ICollection<Result> Results { get; set; }
    }
}
