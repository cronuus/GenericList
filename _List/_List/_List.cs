using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _List
{
    public class _List<T> : IEnumerable<T>
    {
        private const int _defaultCapacity = 4;
        public T[] _items;
        private int _size;
        private int _version;
        static readonly T[] _emptyArray = new T[0];
        public _List()
        {
            _items = _emptyArray;
        }
        public T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)_size)
                {
                    throw new ArgumentException();
                }
                return _items[index];
            }
            set
            {
                if ((uint)index >= (uint)_size)
                {
                    throw new ArgumentException();
                }
                _items[index] = value;
                _version++;
            }
        }
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        T[] newItems = new T[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }
                }
            }
        }
        public int Count
        {
            get
            {
                return _size;
            }
        }
        public void Add(T item)
        {
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            _items[_size++] = item;
            _version++;
        }
        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                if ((uint)newCapacity > 0X7FEFFFFF)
                    newCapacity = 0X7FEFFFFF;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }
        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                _size = 0;
            }
            _version++;
        }
        public bool Contains(T element)
        {
            foreach (var item in _items)
            {
                if (object.Equals(item, element)) return true;
            }

            return false;
        }
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            if (_size - index < count)
            {
                throw new ArgumentException();
            }
            Array.Copy(_items, index, array, arrayIndex, count);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_items, 0, array, arrayIndex, _size);
        }
        public int IndexOf(T item)
        {
            return Array.IndexOf(_items, item, 0, _size);
        }
        public int IndexOf(T item, int index)
        {
            if (index > _size)
                throw new ArgumentOutOfRangeException();
            return Array.IndexOf(_items, item, index, _size - index);
        }
        public int IndexOf(T item, int index, int count)
        {
            if (index > _size)
                throw new ArgumentOutOfRangeException();
            if (count < 0 || index > _size - count) throw new ArgumentOutOfRangeException();
            return Array.IndexOf(_items, item, index, count);
        }
        public void Insert(int index, T item)
        {
            if ((uint)index > (uint)_size)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            if (index < _size)
            {
                Array.Copy(_items, index, _items, index + 1, _size - index);
            }
            _items[index] = item;
            _size++;
            _version++;
        }
        public void InsertRange(int insertionIndex, T[] arrayToInsert)
        {
            if (0 < insertionIndex || insertionIndex > _items.Length)
                throw new ArgumentOutOfRangeException();
            if (_items.Length + arrayToInsert.Length > _size)
            {
                EnsureCapacity(_items.Length + arrayToInsert.Length);
            }

            for (int i = 0; i < arrayToInsert.Length; i++)
            {
                _items[insertionIndex + arrayToInsert.Length + i] = _items[insertionIndex + i];
                _items[insertionIndex + i] = arrayToInsert[i];
            }

            _size += arrayToInsert.Length;
        }
        public int LastIndexOf(T element)
        {
            for (int i = _items.Length - 1; i >= 0; i--)
            {
                if (object.Equals(element, _items[i]))
                    return i;
            }

            return -1;
        }
        public bool Remove(T element)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (object.Equals(element, _items[i]))
                {
                    for (int j = i + 1; j < _items.Length - 1; j++)
                    {
                        _items[j - 1] = _items[j];
                    }

                    _items[_items.Length - 1] = default;
                    _size--;
                    return true;
                }
            }

            return false;
        }
        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)_size)
            {
                throw new ArgumentOutOfRangeException();
            }
            _size--;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
            }
            _items[_size] = default(T);
            _version++;
        }
        public void RemoveRange(int index, int count)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (_size - index < count)
                throw new ArgumentException();

            if (count > 0)
            {
                int i = _size;
                _size -= count;
                if (index < _size)
                {
                    Array.Copy(_items, index + count, _items, index, _size - index);
                }
                Array.Clear(_items, _size, count);
                _version++;
            }
        }
        public void Reverse()
        {
            for (int i = 0; i < _items.Length / 2; i++)
            {
                T tmp = _items[i];
                _items[i] = _items[_items.Length - 1 - i];
                _items[_items.Length - 1 - i] = tmp;
            }
        }
        public T[] ToArray()
        {
            return _items;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(_items, _size);
        }
    }
}
