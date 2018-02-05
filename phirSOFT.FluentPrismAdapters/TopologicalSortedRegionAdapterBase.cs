using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using phirSOFT.TopologicalComparison;
using Prism.Regions;

namespace phirSOFT.FluentPrismAdapters
{
    public abstract class TopologicalSortedRegionAdapterBase<T> : RegionAdapterBase<T> where T : DependencyObject
    {
        public static readonly DependencyProperty ComparerProperty = DependencyProperty.RegisterAttached(
            "Comparer", typeof(ITopologicalComparer), typeof(TopologicalSortedRegionAdapterBase<T>),
            new PropertyMetadata(default(ITopologicalComparer)));

        protected TopologicalSortedRegionAdapterBase(IRegionBehaviorFactory regionBehaviorFactory) : base(
            regionBehaviorFactory)
        {
        }

        public static void SetComparer(DependencyObject element, ITopologicalComparer value)
        {
            element.SetValue(ComparerProperty, value);
        }

        public static ITopologicalComparer GetComparer(DependencyObject element)
        {
            return (ITopologicalComparer) element.GetValue(ComparerProperty);
        }

        protected override void Adapt(IRegion region, T regionTarget)
        {
            region.Views.CollectionChanged += (sender, args) => UpdateRegion(sender, args, regionTarget);
            //region.ActiveViews.CollectionChanged += (sender, args) => ActivateView(sender, args);
        }


        protected virtual void UpdateRegion(object sender, NotifyCollectionChangedEventArgs args,
            T regionTarget)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (DependencyObject item in args.NewItems)
                    {
                        var list = GetItemCollection(regionTarget, item);
                        var comparer = GetComparer(regionTarget);
                        if (comparer != null)
                            list.Insert(item, comparer);
                        else
                            list.Add(item);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (DependencyObject item in args.NewItems)
                    {
                        var list = GetItemCollection(regionTarget, item);
                        list.Remove(item);
                    }

                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        protected abstract IList GetItemCollection(T container);

        protected virtual IList GetItemCollection(T container, DependencyObject item)
        {
            return GetItemCollection(container);
        }

        protected override IRegion CreateRegion()
        {
            return new Region();
        }
    }
}