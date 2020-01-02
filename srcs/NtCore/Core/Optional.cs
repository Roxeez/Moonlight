using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NtCore.Core
{
    public static class Optional
    {
        public static Optional<T> Of<T>([NotNull] T value) => new Optional<T>(value);
        public static Optional<T> Empty<T>() => new Optional<T>();
        public static Optional<T> OfNullable<T>([CanBeNull] T value) => value == null ? new Optional<T>() : new Optional<T>(value);
    }
    
    public struct Optional<T> : IEquatable<Optional<T>>
    {
        private readonly T _value;
        
        internal Optional([CanBeNull] T value) => _value = value;

        [NotNull]
        public T Get()
        {
            if (_value == null)
            {
                throw new InvalidOperationException("Trying to get a non existing value from Optional");
            }

            return _value;
        }

        public void IfPresent([NotNull] Action<T> action)
        {
            if (IsPresent())
            {
                action.Invoke(_value);
            }
        }

        public K IfPresent<K>([NotNull] Func<T, K> action)
        {
            if (IsPresent())
            {
                return action.Invoke(_value);
            }

            return default;
        }
        
        public Task<K> IfPresent<K>([NotNull] Func<T, Task<K>> action)
        {
            if (IsPresent())
            {
                return action.Invoke(_value);
            }

            return default;
        }

        public bool IsPresent() => _value != null;

        public T OrElse([NotNull] T value) => IsPresent() ? _value : value;

        public bool Equals(Optional<T> other)
        {
            if (IsPresent())
            {
                return other.IsPresent() && other.Get().Equals(Get());
            }

            return !other.IsPresent();
        }
    }
}