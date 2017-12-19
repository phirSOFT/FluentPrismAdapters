using System.Windows;
using Fluent;

namespace phirSOFT.FluentRegionAdapters
{
    public class GroupConstraint : PositionConstraint
    {
        public static readonly DependencyProperty GroupProperty = DependencyProperty.RegisterAttached(
            "Group", typeof(int), typeof(GroupConstraint), new PropertyMetadata(default(int)));

        public static void SetGroup(DependencyObject element, int value)
        {
            element.SetValue(GroupProperty, value);
        }

        public static int GetGroup(DependencyObject element)
        {
            return (int) element.GetValue(GroupProperty);
        }

        public override bool CanCompare(DependencyObject x, DependencyObject y)
        {
            return x is RibbonTabItem && y is RibbonTabItem;
        }

        public override int Compare(DependencyObject x, DependencyObject y)
        {
            var left = GetGroup(x);
            var right = GetGroup(y);

            return left - right;
        }
    }
}