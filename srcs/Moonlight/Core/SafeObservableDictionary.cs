using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Moonlight.Core.Extensions;
using Moonlight.Game.Inventories;
using PropertyChanged;

namespace Moonlight.Core
{
    [SuppressPropertyChangedWarnings]
    public class SafeObservableDictionary<K, V> : INotifyPropertyChanged, INotifyCollectionChanged, IEnumerable<V>
    {
        private readonly Dictionary<K, V> _internalDictionary;

        internal IEnumerable<V> Values => _internalDictionary.Values;
        internal Dictionary<K, V> Internal => _internalDictionary;
        
        public SafeObservableDictionary()
        {
            _internalDictionary = new Dictionary<K, V>();
        }
        
        internal V this[K key]
        {
            get => _internalDictionary.GetValueOrDefault(key);
            set => Add(key, value);
        }
        
        internal void Add(K key, V value)
        {
            _internalDictionary[key] = value;
            
            Dispatch(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
            });
        }

        internal void Clear()
        {
            _internalDictionary.Clear();
            
            Dispatch(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            });
        }

        internal void Remove(K key)
        {
            V item = _internalDictionary.GetValueOrDefault(key);
            bool removed = _internalDictionary.Remove(key);
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

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        
        public IEnumerator<V> GetEnumerator() => _internalDictionary.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}