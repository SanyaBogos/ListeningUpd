using Listening.Core.ViewModels.AccountViewModels;
using Listening.Core.ViewModels.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Text
{
    public class TextWithAdminsListDto
    {
        public TextDto TextDto { get; set; }
        public UserViewModel[] Admins { get; set; }
    }
}
