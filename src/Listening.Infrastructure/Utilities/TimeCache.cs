using Listening.Server.Entities.Specialized;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Utilities
{
    public class TimeCache<T>
    {
        private int _maxTimeMs;
        private ConcurrentDictionary<T, DateTime> _dtos = new ConcurrentDictionary<T, DateTime>();

        public TimeCache(int maxTimeMs)
        {
            _maxTimeMs = maxTimeMs;
        }

        // public T GetCached(Y id)
        // {
        //     if (!_dtos.Keys.Any())
        //         return default(T);

        //     var dtoCached = _dtos.Keys.FirstOrDefault(x => x.Id.Equals(id));

        //     if (dtoCached != null)
        //         _dtos[dtoCached] = DateTime.Now;

        //     return dtoCached;
        // }

        // public void Insert(T element)
        // {
        //     RemoveIfExceed();
        //     _dtos[element] = DateTime.Now;
        // }

        // public void Delete(Y id)
        // {
        //     if (_dtos.Keys == null || !_dtos.Keys.Any())
        //         return;

        //     var exist = _dtos.Keys.First(x => x.Id.Equals(id));
        //     _dtos.TryRemove(exist, out DateTime time);
        // }

        // public void Delete(Y[] ids)
        // {
        //     if (_dtos.Keys == null || !_dtos.Keys.Any())
        //         return;

        //     var existItems = _dtos.Keys.Where(x => ids.Contains(x.Id));
        //     foreach (var item in existItems)
        //         _dtos.TryRemove(item, out DateTime time);
        // }

        // public void DeleteAll()
        // {
        //     _dtos.Clear();
        // }

        // private void RemoveIfExceed()
        // {
        //     if (_dtos.Count <= MaxCount)
        //         return;
        //     var forDeleting = _dtos.First(x => x.Value == _dtos.Values.Min());
        //     _dtos.TryRemove(forDeleting.Key, out DateTime time);
        // }
    }
}
