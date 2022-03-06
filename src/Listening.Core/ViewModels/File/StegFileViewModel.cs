using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listening.Core.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Http;

namespace Listening.Core.ViewModels.File
{
    public class StegFileViewModel : BaseCaptchaViewModel
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }        
    }
}
