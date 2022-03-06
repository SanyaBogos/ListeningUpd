using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Listening.Server.Utilities
{
    public class SimpleTimeSpentCache<T> : ISimpleTimeSpentCache<T> where T : IEquatable<T>
    {
        private int MaxCount = 1024;
        private int MaxCheckCounts = 10;
        private ConcurrentDictionary<T, DateTime> _dtos = new ConcurrentDictionary<T, DateTime>();

        public bool UseCache { get; set; }

        public DateTime GetValue(T element)
        {
            CheckIxistance(element);

            DateTime val;
            _dtos.Remove(element, out val);
            return val;
        }

        public void Insert(T element)
        {
            _dtos[element] = DateTime.Now;
        }

        public void DeleteAll()
        {
            _dtos.Clear();
        }

        private void CheckIxistance(T element)
        {
            bool noValues, noKey;
            int index = 0;

            do
            {
                noValues = !_dtos.Keys.Any();
                noKey = !_dtos.Keys.Contains(element);
                Thread.Sleep(1000);
                index++;
            } while ((noValues || noKey) && index < MaxCheckCounts);

            if (noValues)
                throw new Exception("No values!");

            if (noKey)
                throw new Exception("No such time spent key!");
        }
    }
}
