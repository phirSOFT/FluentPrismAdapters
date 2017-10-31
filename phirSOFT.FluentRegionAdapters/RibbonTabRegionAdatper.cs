using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using Fluent;


namespace phirSOFT.FluentRegionAdapters
{
    public class RibbonTabRegionAdatper : RegionAdapterBase<RibbonTabItem>
    {
        public RibbonTabRegionAdatper(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, RibbonTabItem regionTarget)
        {
            throw new NotImplementedException();
        }

        protected override IRegion CreateRegion()
        {
            throw new NotImplementedException();
        }
    }
}
