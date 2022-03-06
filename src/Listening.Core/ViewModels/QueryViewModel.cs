using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels
{
    public class QueryViewModel
    {
        public int Page { get; set; }
        public int ElementsPerPage { get; set; }
        public bool IsAscending { get; set; }
        public string SortingName { get; set; }
        public Dictionary<string, string> FilteringProperties { get; set; }
    }
}
