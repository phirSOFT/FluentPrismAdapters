using System.Collections;
using System.Windows;
using Fluent;
using Prism.Regions;

namespace phirSOFT.FluentPrismAdapters
{
    public class RibbonMenuRegionAdapter : TopologicalSortedRegionAdapterBase<RibbonMenu>
    {
        public RibbonMenuRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override IList GetItemCollection(RibbonMenu container)
        {
            return container.Items;
        }

    }
}