using System.Collections;
using Fluent;
using Prism.Regions;

namespace phirSOFT.FluentPrismAdapters
{
    public class RibbonTabItemRegionAdapter : TopologicalSortedRegionAdapterBase<RibbonTabItem>
    {
        public RibbonTabItemRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override IList GetItemCollection(RibbonTabItem container)
        {
            return container.Groups;
        }
    }
}