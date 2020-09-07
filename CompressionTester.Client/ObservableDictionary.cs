using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChangedAction = System.Collections.Specialized.NotifyCollectionChangedAction;

namespace CompressionTester.Client
{
    public sealed class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        private Dictionary<TKey, TValue> _inner;

        public IEqualityComparer<TKey> Comparer => _inner.Comparer;
        public int Count => _inner.Count;
        public ICollection<TKey> Keys => _inner.Keys;
        public ICollection<TValue> Values => _inner.Values;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private void OnCollectionChanged(ChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }

        public ObservableDictionary()
        {
            _inner = new Dictionary<TKey, TValue>();
        }
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _inner = new Dictionary<TKey, TValue>(dictionary);
        }
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            _inner = new Dictionary<TKey, TValue>(dictionary, comparer);
        }
        public ObservableDictionary(int capacity)
        {
            _inner = new Dictionary<TKey, TValue>(capacity);
        }
        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            _inner = new Dictionary<TKey, TValue>(capacity, comparer);
        }
        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            _inner = new Dictionary<TKey, TValue>(comparer);
        }

        public void Add(TKey key, TValue value)
        {
            _inner.Add(key, value);
            OnCollectionChanged(ChangedAction.Add);
        }

        public void Clear()
        {
            _inner.Clear();
            OnCollectionChanged(ChangedAction.Reset);
        }

        public bool Remove(TKey key)
        {
            var result = _inner.Remove(key);
            if (result) OnCollectionChanged(ChangedAction.Remove);
            return result;
        }

        public bool ContainsKey(TKey key) => _inner.ContainsKey(key);
        public bool ContainsValue(TValue value) => _inner.ContainsValue(value);
        public bool TryGetValue(TKey key, out TValue value) => _inner.TryGetValue(key, out value);

        public TValue this[TKey key]
        {
            get => _inner[key];
            set
            {
                var containedKey = _inner.ContainsKey(key);
                _inner[key] = value;
                OnCollectionChanged(containedKey ? ChangedAction.Replace : ChangedAction.Add);
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)_inner).Add(keyValuePair);
            OnCollectionChanged(ChangedAction.Add);
        }
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)_inner).Contains(keyValuePair);
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] keyValuePairs, int arrayIndex) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)_inner).CopyTo(keyValuePairs, arrayIndex);

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            var result = ((ICollection<KeyValuePair<TKey, TValue>>)_inner).Remove(keyValuePair);
            if (result) OnCollectionChanged(ChangedAction.Remove);
            return result;
        }
            
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly =>
            ((ICollection<KeyValuePair<TKey, TValue>>)_inner).IsReadOnly;

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
            ((IEnumerable<KeyValuePair<TKey, TValue>>) _inner).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _inner).GetEnumerator();
    }
}
