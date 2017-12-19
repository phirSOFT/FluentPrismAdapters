using System.Windows;
using CommonServiceLocator;
using Fluent;
using phirSOFT.FluentRegionAdapters;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace phirSOFT.FluentRibbonRegionAdapters.SampleApplicatio
{
    internal class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow?.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleAuthType = typeof(ModuleA);
            ModuleCatalog.AddModule(new ModuleInfo(moduleAuthType.Name, moduleAuthType.AssemblyQualifiedName));
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(Ribbon), ServiceLocator.Current.GetInstance<RibbonRegionAdapter>());
            return mappings;
        }
    }
}