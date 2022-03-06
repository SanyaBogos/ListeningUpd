using Listening.Core.ViewModels.AccountViewModels;
using Listening.Core.ViewModels.ConstraintsInterface.AccountViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Listening.Core.ViewModels.OCR
{
    public class ImageParamsViewModel : BaseCaptchaViewModel
    {
        [Required]
        public string Base64 { get; set; }
    }
}
