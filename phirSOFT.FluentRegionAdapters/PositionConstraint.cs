using System.Windows;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.FluentRegionAdapters
{
    public abstract class PositionConstraint : ITopologicalComparer<DependencyObject>, ITopologicalComparer
    {
        public int Compare(object x, object y)
        {
            return Compare(x as DependencyObject, y as DependencyObject);
        }

        public bool CanCompare(object x, object y)
        {
            return x is DependencyObject xx && y is DependencyObject yy && CanCompare(xx, yy);
        }

        public abstract bool CanCompare(DependencyObject x, DependencyObject y);

        public abstract int Compare(DependencyObject x, DependencyObject y);
    }
}