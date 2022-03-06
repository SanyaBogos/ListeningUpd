using Listening.Core.ViewModels.ConstraintsInterface.AccountViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Listening.Core.ViewModels.AccountViewModels
{
    public class BaseCaptchaViewModel : ICaptchaViewModel
    {
        public CaptchaCheckDto Captcha { get; set; }
    }
}
