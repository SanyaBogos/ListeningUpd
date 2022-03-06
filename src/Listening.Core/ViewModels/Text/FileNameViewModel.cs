using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.Text
{
    public class FileNameViewModel
    {
        public string Name { get; set; }
        public FileNameViewModel(string name)
        {
            Name = name;
        }
    }
}
