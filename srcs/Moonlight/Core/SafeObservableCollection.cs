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

        public SafeObservableCollection() => _collection = new Collection<T>();

        public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

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
            bool removed = _collection.Remove(item);
            if (!removed)
            {
                return false;
            }

            Dispatch(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            });
            return true;
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
    }
}