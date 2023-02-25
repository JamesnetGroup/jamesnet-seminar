using CommunityToolkit.Mvvm.ComponentModel;
using James.Core;
using Prism.Ioc;
using Prism.Regions;

namespace James.Forms.Local.ViewModels
{
    public class MainContentViewModel : ObservableObject
    {
        private readonly IContainerProvider _containerProvider;

        public string Name { get; init; }

        public MainContentViewModel(IContainerProvider containerProvider, IRegionManager regionManager, JamesClass james)
        {
            james.Name = "123123";

            _containerProvider = containerProvider;
            Name = "James";

            var a = containerProvider.Resolve<PrismContent>("UserContent");
        }
    }
}
