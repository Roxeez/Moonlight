using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace Moonlight.Core.Collection
{
    public class InternalObservableList<T> : IEnumerable<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        protected ThreadSafeList<T> ThreadSafeInternalList { get; } = new ThreadSafeList<T>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count => ThreadSafeInternalList.Count;

        internal void Add(T item)
        {
            ThreadSafeInternalList.Add(item);
            
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            });
        }

        internal void Remove(T item)
        {
            bool removed = ThreadSafeInternalList.Remove(item);
            if (!removed)
            {
                return;
            }
            
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            });
        }

        internal void Clear()
        {
            ThreadSafeInternalList.Clear();
            
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            });
        }

        public bool Contains(T item) => ThreadSafeInternalList.Contains(item);

        public IEnumerator<T> GetEnumerator() => ThreadSafeInternalList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}