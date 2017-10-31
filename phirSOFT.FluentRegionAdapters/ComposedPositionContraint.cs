using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace phirSOFT.FluentRegionAdapters
{
    public abstract class ComposedPositionContraint<T> : PositionConstraint<T>, ICollection<PositionConstraint<T>>
    {
        private ICollection<PositionConstraint<T>> _collectionImplementation;

        public IEnumerator<PositionConstraint<T>> GetEnumerator()
        {
            return _collectionImplementation.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _collectionImplementation).GetEnumerator();
        }

        public void Add(PositionConstraint<T> item)
        {
            _collectionImplementation.Add(item);
        }

        public void Clear()
        {
            _collectionImplementation.Clear();
        }

        public bool Contains(PositionConstraint<T> item)
        {
            return _collectionImplementation.Contains(item);
        }

        public void CopyTo(PositionConstraint<T>[] array, int arrayIndex)
        {
            _collectionImplementation.CopyTo(array, arrayIndex);
        }

        public bool Remove(PositionConstraint<T> item)
        {
            return _collectionImplementation.Remove(item);
        }

        public int Count
        {
            get { return _collectionImplementation.Count; }
        }

        public bool IsReadOnly
        {
            get { return _collectionImplementation.IsReadOnly; }
        }

        protected override bool CanCompare(T left, T right)
        {
            return this.Any(c => CanCompare(c, left, right));
        }

        public override int Compare(T x, T y)
        {
            return this.First(c => CanCompare(c, x, y)).Compare(x, y);
        }

    }
}