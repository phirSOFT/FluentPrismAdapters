using System.Collections.Generic;
using System.Windows.Controls;
using CommonServiceLocator;
using Fluent;
using phirSOFT.FluentRegionAdapters;
using Prism.Modularity;
using Prism.Regions;

namespace phirSOFT.FluentRibbonRegionAdapters.SampleApplicatio
{
    internal class ModuleA : IModule
    {
        public void Initialize()
        {
            var manager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var tab1 = new RibbonTabItem {Header = "1"};
            var tab2 = new RibbonTabItem {Header = "2"};
            var tab3 = new RibbonTabItem {Header = "3"};
            var tab4 = new RibbonTabItem {Header = "4"};

            GroupConstraint.SetGroup(tab1, 1);
            GroupConstraint.SetGroup(tab2, 2);
            GroupConstraint.SetGroup(tab3, 3);
            GroupConstraint.SetGroup(tab4, 4);

            var additionalTabs = new HashSet<RibbonTabItem>()
            {
                tab1, tab2, tab3, tab4
            };

            for (var i = 1; i < 5; i++)
            {
                var list = new List<RibbonTabItem>();
                for (var j = 0; j < 5; j++)
                {
                    var tab = new RibbonTabItem { Header = $"{i}.{j}" };
                    GroupConstraint.SetGroup(tab, i);

                    foreach (var ribbonTabItem in list)
                    {
                        RelativeContraint.GetAfter(tab).Add(ribbonTabItem);
                    }

                    list.Add(tab);
                    additionalTabs.Add(tab);

                }

               
            }

    


            RelativeContraint.GetBefore(tab1).Add(tab2);
            RelativeContraint.GetBefore(tab1).Add(tab4);

            RelativeContraint.GetBefore(tab2).Add(tab3);
            RelativeContraint.GetAfter(tab2).Add(tab1);

            RelativeContraint.GetBefore(tab3).Add(tab4);
            RelativeContraint.GetAfter(tab3).Add(tab1);

            RelativeContraint.GetAfter(tab4).Add(tab2);
            RelativeContraint.GetAfter(tab4).Add(tab3);


            var inex = 0;
            foreach (var additionalTab in additionalTabs)
            {
               var box = new RibbonGroupBox();
                box.Items.Add(new Label {Content = inex});
                additionalTab.Groups.Add(box);
                manager.AddToRegion("Ribbon", additionalTab);
                ++inex;
            }
        }
    }
}