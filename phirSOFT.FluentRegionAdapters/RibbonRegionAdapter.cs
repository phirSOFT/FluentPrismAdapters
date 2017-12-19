using System;
using System.Collections;
using System.Windows;
using Fluent;
using Prism.Regions;

namespace phirSOFT.FluentRegionAdapters
{
    public class RibbonRegionAdapter : ContraintedRegionAdapter<Ribbon>
    {
        public RibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
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
                case UIElement _:
                    return container.ToolBarItems;
            }
            return null;
        }
    }
}