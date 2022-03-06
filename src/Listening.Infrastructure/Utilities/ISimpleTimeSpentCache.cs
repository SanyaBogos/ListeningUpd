using System;

namespace Listening.Server.Utilities
{
    public interface ISimpleTimeSpentCache<T> where T : IEquatable<T>
    {
        bool UseCache { get; set; }

        DateTime GetValue(T element);
        void Insert(T element);
        void DeleteAll();
    }
}