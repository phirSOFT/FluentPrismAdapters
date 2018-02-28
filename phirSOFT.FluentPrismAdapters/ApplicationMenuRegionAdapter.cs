using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluent;
using Prism.Regions;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.FluentPrismAdapters
{
    /// <summary>
    /// Provides a region adadpter dor an application menu.
    /// </summary>
    public class ApplicationMenuRegionAdapter : TopologicalSortedRegionAdapterBase<ApplicationMenu>
    {
        public ApplicationMenuRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override IList GetItemCollection(ApplicationMenu container)
        {
            return container.Items;
        }

        public static void DeepMerge(ApplicationMenu target, ApplicationMenu source)
        {
            var comparer = GetComparer(target);
            foreach (var sourceItem in source.Items)
            {
                if (comparer != null)
                    target.Items.Insert(sourceItem, comparer);
                else
                    target.Items.Add(sourceItem);
            }
        }
    }
}
