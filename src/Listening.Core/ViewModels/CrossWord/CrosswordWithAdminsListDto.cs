using Listening.Core.ViewModels.AccountViewModels;
using Listening.Core.ViewModels.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.CrossWord
{
    public class CrosswordWithAdminsListDto
    {
        public CrosswordDto Crossword { get; set; }
        public UserViewModel[] Admins { get; set; }
    }
}
