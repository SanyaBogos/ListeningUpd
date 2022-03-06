using Listening.Core.ViewModels.ConstraintsInterface.AccountViewModels;
using System.ComponentModel.DataAnnotations;

namespace Listening.Core.ViewModels.AccountViewModels
{
    public class RegisterViewModel : BaseCaptchaViewModel, IViewModel
    {
        //[Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        //[Required]
        [StringLength(250)]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        //[Required]
        [StringLength(250)]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(250)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Phone]
        public string Mobile { get; set; }

        //[Required]
        //public string Captcha { get; set; }
    }
}
