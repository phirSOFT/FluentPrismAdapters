using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using phirSOFT.TopologicalComparison;
using Prism.Regions;

namespace phirSOFT.FluentRegionAdapters
{
    public abstract class ContraintedRegionAdapter<TContainer> : RegionAdapterBase<TContainer>
        where TContainer : DependencyObject
    {
        public static readonly DependencyProperty ConstraintComparerProperty = DependencyProperty.RegisterAttached(
            "ConstraintComparer", typeof(ITopologicalComparer), typeof(ContraintedRegionAdapter<TContainer>),
            new PropertyMetadata(new DefaultComparer()));

        protected ContraintedRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        public static void SetConstraintComparer(DependencyObject element, ITopologicalComparer value)
        {
            element.SetValue(ConstraintComparerProperty, value);
        }

        public static ITopologicalComparer GetConstraintComparer(DependencyObject element)
        {
            return (ITopologicalComparer) element.GetValue(ConstraintComparerProperty);
        }


        protected override void Adapt(IRegion region, TContainer regionTarget)
        {
            region.Views.CollectionChanged += (sender, args) => UpdateRegion(sender, args, regionTarget);
            //region.ActiveViews.CollectionChanged += (sender, args) => ActivateView(sender, args);
        }


        protected virtual void UpdateRegion(object sender, NotifyCollectionChangedEventArgs args,
            TContainer regionTarget)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (DependencyObject item in args.NewItems)
                    {
                        var list = GetItemCollection(regionTarget, item);
                        list.Insert(item, GetConstraintComparer(regionTarget));
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
            }
        }


        protected abstract IList GetItemCollection(TContainer container);

        protected virtual IList GetItemCollection(TContainer container, DependencyObject item)
        {
            return GetItemCollection(container);
        }

        protected override IRegion CreateRegion()
        {
            return new Region();
        }

        private class DefaultComparer : ITopologicalComparer
        {
            public int Compare(object x, object y)
            {
                return 0;
            }

            public bool CanCompare(object x, object y)
            {
                return true;
            }
        }
    }
}