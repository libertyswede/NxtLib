using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#if NET40
namespace NxtLib.Internal
{
    public interface IReadOnlyCollection<out T> : IEnumerable<T>
    {
        int Count { get; }
    }

    public interface IReadOnlyList<out T> : IReadOnlyCollection<T>
    {
        T this[int index] { get; }
    }

    public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        bool ContainsKey(TKey key);
        bool TryGetValue(TKey key, out TValue value);

        TValue this[TKey key] { get; }
        IEnumerable<TKey> Keys { get; }
        IEnumerable<TValue> Values { get; }
    }

    [DebuggerDisplay("Count = {Count}")]
    public class ReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> source;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionaryToWrap)
        {
            if (dictionaryToWrap == null)
            {
                throw new ArgumentNullException("dictionaryToWrap");
            }

            source = dictionaryToWrap;
        }

        public int Count
        {
            get { return source.Count; }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get { return source.Keys; }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get { return source.Values; }
        }

        public ICollection<TKey> Keys
        {
            get { return source.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return source.Values; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public TValue this[TKey key]
        {
            get { return source[key]; }
            set { ThrowNotSupportedException(); }
        }

        public bool ContainsKey(TKey key)
        {
            return source.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return source.TryGetValue(key, out value);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            IEnumerable<KeyValuePair<TKey, TValue>> enumerator = source;

            return enumerator.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return source.GetEnumerator();
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException("This Dictionary is read-only");
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)source).GetEnumerator();
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotSupportedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotSupportedException();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return source.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }
    }
}
#endif