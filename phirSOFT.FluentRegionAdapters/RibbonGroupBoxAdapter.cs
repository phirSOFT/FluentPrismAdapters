using System.Collections;
using Fluent;
using Prism.Regions;

namespace phirSOFT.FluentRegionAdapters
{
    public class RibbonGroupBoxAdapter : ContraintedRegionAdapter<RibbonTabItem>
    {
        public RibbonGroupBoxAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override IList GetItemCollection(RibbonTabItem container)
        {
            return container.Groups;
        }
    }
}