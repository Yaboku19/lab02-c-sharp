namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private readonly IList<TItem> _elements = new List<TItem>();
        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => this._elements.Count;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => _elements.IsReadOnly;

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get => _elements[index];
            set {
                var old = _elements[index];
                _elements[index] = value;
                ElementChanged?.Invoke(this, value, old, index);
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator() => this._elements.GetEnumerator();

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this._elements).GetEnumerator();

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            _elements.Add(item);
            ElementInserted?.Invoke(this, item, Count - 1);
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            for(var i = (Count - 1); i > 0; i--)
            {
                Remove(_elements[i]);
            }
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item) => _elements.Contains(item);

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex) => _elements.CopyTo(array, arrayIndex);

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            ElementRemoved?.Invoke(this, item, IndexOf(item));
            return _elements.Remove(item);
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item) => _elements.IndexOf(item);

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            this[index] = item;
            ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            Remove(this[index]);
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            // TODO improve
            return base.Equals(obj);
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            // TODO improve
            return base.GetHashCode();
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            // TODO improve
            return base.ToString();
        }
    }
}
