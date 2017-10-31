using System.Collections.Generic;

namespace phirSOFT.FluentRegionAdapters
{
    public abstract class PositionConstraint<T> : Comparer<T>
    {
        protected abstract bool CanCompare(T left, T right);

        protected static bool CanCompare(PositionConstraint<T> comparer, T left, T rigt)
        {
            return comparer.CanCompare(left, rigt);
        }
    }
}