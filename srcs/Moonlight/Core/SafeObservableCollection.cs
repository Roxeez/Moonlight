using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Moonlight.Core
{
    public class SafeObservableCollection<T> : IEnumerable<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        private readonly Collection<T> _collection;

        public SafeObservableCollection()
        {
            _collection = new Collection<T>();
        }
        
        internal void Add(T item)
        {
            _collection.Add(item);

            Dispatch(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            });
        }

        internal void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        internal void Clear()
        {
            _collection.Clear();

            Dispatch(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            });
        }

        public bool Contains(T item) => _collection.Contains(item);

        internal bool Remove(T item)
        {
            int index = _collection.IndexOf(item);
            if (index < 0)
            {
                return false;
            }
            
            _collection.RemoveAt(index);

            Dispatch(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            });
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        
        public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Dispatch(Action action)
        {
            Dispatcher dispatcher = Application.Current?.Dispatcher;
            if (dispatcher == null)
            {
                return;
            }

            dispatcher.Invoke(action);
        }
    }
}