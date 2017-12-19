using System;
using System.Collections.Generic;
using System.Windows;

namespace phirSOFT.FluentRegionAdapters
{
    public class RelativeContraint : PositionConstraint
    {
        public static readonly DependencyProperty Before =
            DependencyProperty.RegisterAttached("Before", typeof(IList<DependencyObject>), typeof(DependencyObject),
                new PropertyMetadata(null));

        public static readonly DependencyProperty After =
            DependencyProperty.RegisterAttached("After", typeof(IList<DependencyObject>), typeof(DependencyObject),
                new PropertyMetadata(null));

        public override bool CanCompare(DependencyObject left, DependencyObject right)
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

        public static IList<DependencyObject> GetBefore(DependencyObject dependencyObject)
        {
            var list = (IList<DependencyObject>) dependencyObject.GetValue(Before);
            if (list != null) return list;

            list = new List<DependencyObject>();
            dependencyObject.SetValue(Before, list);
            return list;
        }


        public static IList<DependencyObject> GetAfter(DependencyObject dependencyObject)
        {
            var list = (IList<DependencyObject>) dependencyObject.GetValue(After);
            if (list != null) return list;

            list = new List<DependencyObject>();
            dependencyObject.SetValue(After, list);
            return list;
        }


        public override int Compare(DependencyObject x, DependencyObject y)
        {
            var xBefore = (IList<DependencyObject>) x?.GetValue(Before);
            var xAfter = (IList<DependencyObject>) x?.GetValue(After);
            var yBefore = (IList<DependencyObject>) y?.GetValue(Before);
            var yAfter = (IList<DependencyObject>) y?.GetValue(After);

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