using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebCore.Utils.ModelHelper
{
    public class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        private readonly TKey _key;
        private IEnumerable<TElement> elements;

        public Grouping(TKey key, IEnumerable<TElement> elements)
        {
            _key = key;
            this.elements = elements;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TKey Key => _key;
    }
}
