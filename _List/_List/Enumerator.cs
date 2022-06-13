using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _List
{
    public class Enumerator<T> : IEnumerator<T>
    {
        private readonly T[] _items;
        private int _index = -1;
        private int _size;

        public Enumerator(T[] items, int size)
        {
            _items = items;
            _size = size;
        }

        public bool MoveNext()
        {
            return ++_index < _size;
        }

        public void Reset()
        {
            _index = -1;
        }

        public T Current => _items[_index];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}
