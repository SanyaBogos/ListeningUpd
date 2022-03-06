using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Entities.Specialized.ServiceModels
{
    public class PagedData<T>
    {
        public long Count { get; set; }
        public T[] Data { get; set; }
    }
}
