using Listening.Core.ViewModels.AccountViewModels;
using Listening.Core.ViewModels.ListeningResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.ListeningResult
{
    public class ResultEnhancedDto
    {
        public ResultDto[] Results { get; set; }
        public UserViewModel[] Users { get; set; }
    }
}
