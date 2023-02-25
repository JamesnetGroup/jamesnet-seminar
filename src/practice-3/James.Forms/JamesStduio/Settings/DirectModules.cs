using James.Core;
using James.Forms.UI.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamesStduio.Settings
{
    internal class DirectModules : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {            
            IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion("MainRegion", ContentName.MainContent);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
