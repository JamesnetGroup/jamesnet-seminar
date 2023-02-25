using CommunityToolkit.Mvvm.ComponentModel;
using James.Core;
using Prism.Ioc;
using Prism.Regions;

namespace James.Forms.Local.ViewModels
{
    public class MainContentViewModel : ObservableObject, ILoadable
    {
        private readonly IContainerExtension _container;

        public string Name { get; init; }

        public MainContentViewModel(IContainerExtension container, IRegionManager regionManager, JamesClass james)
        {
            james.Name = "123123";

            _container = container;
            Name = "James";
        }

        public void OnLoaded(PrismContent view)
        {
            var a1 = _container.Resolve<PrismContent>("MainContent");
            var a3 = _container.Resolve<PrismContent>("MainContent");
            var a2 = _container.Resolve<PrismContent>("UserContent");
        }
    }
}
