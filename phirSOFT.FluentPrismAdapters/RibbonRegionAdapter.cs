using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using Fluent;
using phirSOFT.TopologicalComparison;
using Prism.Regions;

namespace phirSOFT.FluentPrismAdapters
{
    public class RibbonRegionAdapter : TopologicalSortedRegionAdapterBase<Ribbon>
    {
        protected RibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override IList GetItemCollection(Ribbon container)
        {
            throw new NotImplementedException();
        }

        protected override IList GetItemCollection(Ribbon container, DependencyObject item)
        {
            switch (item)
            {
                case RibbonTabItem _:
                    return container.Tabs;
                case QuickAccessMenuItem _:
                    return container.QuickAccessItems;
                case RibbonContextualTabGroup _:
                    return container.ContextualGroups;
                case Ribbon ribbon:
                    DeepMerge(container, ribbon);
                    break;
                case BackstageTabItem _:
                    return ((container.Menu as Backstage)?.Content as BackstageTabControl)?.Items;
                case UIElement _:
                    return container.ToolBarItems;
            }
            return null;
        }

        private static void DeepMerge(Ribbon target, Ribbon source)
        {
            
        }
    }
}