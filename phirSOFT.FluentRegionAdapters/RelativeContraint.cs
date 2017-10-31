using System;
using System.Collections.Generic;
using System.Windows;

namespace phirSOFT.FluentRegionAdapters
{
    public class RelativeContraint<T> : PositionConstraint<T> where T : DependencyObject
    {
        public readonly DependencyProperty Before =
            DependencyProperty.RegisterAttached("Before", typeof(IList<T>), typeof(T));

        public readonly DependencyProperty After =
            DependencyProperty.RegisterAttached("Before", typeof(IList<T>), typeof(T));

        protected override bool CanCompare(T left, T right)
        {
            try
            {
                return Compare(left, right) != 0;
            }
            catch (Exception)
            {
                return false;
                
            }
           
        }

        public override int Compare(T x, T y)
        {
            var xBefore = (IList<T>) x?.GetValue(Before);
            var xAfter = (IList<T>)x?.GetValue(After);
            var yBefore = (IList<T>) y?.GetValue(Before);
            var yAfter = (IList<T>)y?.GetValue(After);
            
            // x > y
            var xGy = xAfter?.Contains(y) ?? false;

            // x < y
            var xLy = xBefore?.Contains(y) ?? false;

            // y > x
            var yGx = yAfter?.Contains(x) ?? false;

            // y < x
            var yLx = yBefore?.Contains(x) ?? false;

            if (xGy && xLy || xGy && yGx || xLy && yLx || yGx && yLx)
                throw new Exception("Circular ordering");

            xGy |= yLx;
            xLy |= yGx;

            if (xGy)
                return 1;
            if (xLy)
                return -1;
            return 0;

        }
    }
}