using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace phirSOFT.FluentRegionAdapters
{
    public class ComposedPositionContraint : PositionConstraint, IList<PositionConstraint>, IList
    {
        private readonly Collection<PositionConstraint> _collectionImplementation =
            new Collection<PositionConstraint>();

        public int Add(object value)
        {
            return ((IList) _collectionImplementation).Add(value);
        }

        public bool Contains(object value)
        {
            return ((IList) _collectionImplementation).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList) _collectionImplementation).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            ((IList) _collectionImplementation).Insert(index, value);
        }

        public void Remove(object value)
        {
            ((IList) _collectionImplementation).Remove(value);
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection) _collectionImplementation).CopyTo(array, index);
        }

        public object SyncRoot => ((ICollection) _collectionImplementation).SyncRoot;

        public bool IsSynchronized => ((ICollection) _collectionImplementation).IsSynchronized;

        public bool IsFixedSize => ((IList) _collectionImplementation).IsFixedSize;

        object IList.this[int index]
        {
            get => ((IList) _collectionImplementation)[index];
            set => ((IList) _collectionImplementation)[index] = value;
        }

        public IEnumerator<PositionConstraint> GetEnumerator()
        {
            return _collectionImplementation.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _collectionImplementation).GetEnumerator();
        }

        public void Add(PositionConstraint item)
        {
            _collectionImplementation.Add(item);
        }

        public void Clear()
        {
            _collectionImplementation.Clear();
        }

        public bool Contains(PositionConstraint item)
        {
            return _collectionImplementation.Contains(item);
        }

        public void CopyTo(PositionConstraint[] array, int arrayIndex)
        {
            _collectionImplementation.CopyTo(array, arrayIndex);
        }

        public bool Remove(PositionConstraint item)
        {
            return _collectionImplementation.Remove(item);
        }

        public int Count => _collectionImplementation.Count;

        public bool IsReadOnly => false;

        public int IndexOf(PositionConstraint item)
        {
            return _collectionImplementation.IndexOf(item);
        }

        public void Insert(int index, PositionConstraint item)
        {
            _collectionImplementation.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _collectionImplementation.RemoveAt(index);
        }

        public PositionConstraint this[int index]
        {
            get => _collectionImplementation[index];
            set => _collectionImplementation[index] = value;
        }

        public override bool CanCompare(DependencyObject left, DependencyObject right)
        {
            return this.Any(c => c.CanCompare(left, right));
        }

        public override int Compare(DependencyObject x, DependencyObject y)
        {
            var result = 0;
            return _collectionImplementation.Any(comparer => comparer.CanCompare(x, y) && (result = comparer.Compare(x, y)) != 0) ? result : 0;
        }
    }
}