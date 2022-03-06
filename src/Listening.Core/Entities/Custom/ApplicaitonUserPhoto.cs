using System.ComponentModel.DataAnnotations;

namespace Listening.Core.Entities.Custom
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUserPhoto : IEntityBase
    {

        [Key]
        public long Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
