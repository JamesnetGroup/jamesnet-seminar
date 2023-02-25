using James.Forms.Local.ViewModels;
using James.Forms.UI.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JamesStduio
{
    public class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return new MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // containerRegistry.RegisterSingleton : 인스턴스 선언 (필요한 시점에 생성됨)
            // containerRegistry.RegisterInstance : 인스턴스 생성
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            // 추후작업
        }

        internal App WireViewModel()
        {
            ViewModelLocationProvider.Register<MainContent, MainContentViewModel>();
            return this;
        }
    }
}
