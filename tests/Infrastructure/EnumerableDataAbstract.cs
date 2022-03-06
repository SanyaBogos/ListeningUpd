using System.Collections;
using System.Collections.Generic;

namespace Infrastructure
{
    public abstract class EnumerableDataAbstract : IEnumerable<object[]>
    {
        protected List<object[]> _data = new List<object[]>();

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
