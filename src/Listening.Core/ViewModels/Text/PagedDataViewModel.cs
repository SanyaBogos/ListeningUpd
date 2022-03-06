using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.Text
{
    public class PagedDataViewModel<T>
    {
        public long Count { get; set; }
        public T[] Data { get; set; }
    }
}
