using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Moonlight.Core.Extensions;
using PropertyChanged;

namespace Moonlight.Core
{
    [SuppressPropertyChangedWarnings]
    public class SafeObservableDictionary<K, V> : INotifyPropertyChanged, INotifyCollectionChanged, IEnumerable<V>
    {
        public SafeObservableDictionary() => Internal = new Dictionary<K, V>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        
        internal IEnumerable<V> Values => Internal.Values;
        internal Dictionary<K, V> Internal { get; }

        internal V this[K key]
        {
            get => Internal.GetValueOrDefault(key);
            set => Add(key, value);
        }

        public int Count => Internal.Count;

        internal void Add(K key, V value)
        {
            Internal[key] = value;

            Dispatch(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
            });
        }

        internal void Clear()
        {
            Internal.Clear();

            Dispatch(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            });
        }

        internal void Remove(K key)
        {
            V item = Internal.GetValueOrDefault(key);
            bool removed = Internal.Remove(key);
            if (!removed)
            {
                return;
            }

            Dispatch(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            });
        }

        private void Dispatch(Action action)
        {
            Dispatcher dispatcher = Application.Current?.Dispatcher;
            if (dispatcher == null)
            {
                return;
            }

            dispatcher.Invoke(action);
        }
        
        public IEnumerator<V> GetEnumerator() => Internal.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}