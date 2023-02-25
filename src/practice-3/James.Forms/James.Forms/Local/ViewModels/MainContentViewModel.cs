using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using James.Core;
using James.Core.Events;
using James.Core.Models;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using System;

namespace James.Forms.Local.ViewModels
{
    public partial class MainContentViewModel : ObservableObject, ILoadable
    {
        private readonly IContainerExtension _container;
        private readonly IEventAggregator _ea;

        public string Name { get; init; }

        [ObservableProperty]
        private int _number;

        public MainContentViewModel(IEventAggregator ea, IContainerExtension container, IRegionManager regionManager, JamesClass james)
        {
            _container = container;
            _ea = ea;

            james.Name = "123123";

            Name = "James";

            _ea.GetEvent<PubSubEvent<int>>().Subscribe(Received);
        }

        private void Received(int obj)
        {
            Number = obj;
        }

        public void OnLoaded(PrismContent view)
        {
            var a1 = _container.Resolve<PrismContent>("MainContent");
            var a3 = _container.Resolve<PrismContent>("MainContent");
            var a2 = _container.Resolve<PrismContent>("UserContent");
        }

        [RelayCommand]
        private void Send()
        {
            MessageModel model = new();
            model.Name = "James1";
            _ea.GetEvent<MessageEvent>().Publish(model);
        }
    }
}
