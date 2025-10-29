namespace Schematic.RulesEngine.Utils
{
    public class Set<T>
    {
        private HashSet<T> _items = new HashSet<T>();

        public Set(params T[] items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    _items.Add(item);
                }
            }
        }

        public static Set<T> NewSet(params T[] items)
        {
            return new Set<T>(items);
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public int Count => _items.Count;

        public Set<T> Intersection(Set<T> other)
        {
            var result = new Set<T>();
            result._items = new HashSet<T>(_items.Intersect(other._items));
            return result;
        }

        public int Len => Count;
    }
}