using Listening.Core.ViewModels.ConstraintsInterface.AccountViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Listening.Core.ViewModels
{
    public class CaptchaDto
    {
        public string Name { get; set; }
        public string Hash { get; set; }
    }
}
