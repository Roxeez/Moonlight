using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using Moonlight.Core.Extensions;
using PropertyChanged;

namespace Moonlight.Core.Collection
{
    [SuppressPropertyChangedWarnings]
    public class InternalObservableDictionary<K, V> : IEnumerable<V>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        protected ThreadSafeDictionary<K, V> ThreadSafeInternalDictionary { get; } = new ThreadSafeDictionary<K, V>();
        
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count => ThreadSafeInternalDictionary.Count;
        internal ICollection<V> Values => ThreadSafeInternalDictionary.Values;

        internal V this[K key]
        {
            get => ThreadSafeInternalDictionary[key];
            set
            {
                ThreadSafeInternalDictionary[key] = value; 
                
                Application.Current?.Dispatcher?.Invoke(() =>
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
                });
            }
        }
        
        internal void Add(K key, V value)
        {
            ThreadSafeInternalDictionary.Add(key, value);
            
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
            });
        }

        internal void Remove(K key)
        {
            if (!ThreadSafeInternalDictionary.TryGetValue(key, out V value))
            {
                return;
            }

            ThreadSafeInternalDictionary.Remove(key);
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value));
            });
        }

        internal void Clear()
        {
            ThreadSafeInternalDictionary.Clear();
            
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            });
        }

        internal V GetValueOrDefault(K key) => ThreadSafeInternalDictionary.GetValueOrDefault(key);

        public bool ContainsKey(K key) => ThreadSafeInternalDictionary.ContainsKey(key);

        public IEnumerator<V> GetEnumerator() => ThreadSafeInternalDictionary.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}