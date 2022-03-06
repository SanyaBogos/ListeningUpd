using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.Text
{
    public class CorrectWordLocatorsDto
    {
        public string Word { get; set; }
        public WordLocatorDto[] Locators { get; set; }
    }
}
