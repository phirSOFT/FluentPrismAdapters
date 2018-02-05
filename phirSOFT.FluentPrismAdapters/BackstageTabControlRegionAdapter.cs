using System.Collections;
using Fluent;
using Prism.Regions;

namespace phirSOFT.FluentPrismAdapters
{
    public class BackstageTabControlRegionAdapter : TopologicalSortedRegionAdapterBase<BackstageTabControl>
    {
        public BackstageTabControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override IList GetItemCollection(BackstageTabControl container)
        {
            return container.Items;
        }
    }
}