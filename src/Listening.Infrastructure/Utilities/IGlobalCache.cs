using System;
using Listening.Server.Entities.Specialized;

namespace Listening.Server.Utilities
{
    public interface IGlobalCache<T, Y>
        where T : IIdenticable<Y>
        where Y : IEquatable<Y>
    {
        bool UseCache { get; set; }

        void Delete(Y id);
        void Delete(Y[] ids);
        void DeleteAll();
        T GetCached(Y id);
        void Insert(T element);
    }
}