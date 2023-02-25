using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using James.Core;
using James.Core.Events;
using James.Core.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace James.Users.Local.ViewModels
{
    public partial class UserContentViewModel : ObservableObject, ILoadable
    {
        private readonly IEventAggregator _ea;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RequestCommand))]
        private MessageModel _message;

        [ObservableProperty]
        private string _messageCompleted;

        public UserContentViewModel(JamesClass james, IEventAggregator ea) 
        {
            _ea = ea;

            _ea.GetEvent<MessageEvent>().Subscribe(Send);
        }

        public void OnLoaded(PrismContent view)
        {
            
        }

        private void Send(MessageModel message)
        {
            Message = message;
        }

        private bool CanRequest()
        {
            return Message != null;
        }

        [RelayCommand(CanExecute = nameof(CanRequest))]
        private void Request()
        {
            _ea.GetEvent<PubSubEvent<int>>().Publish(111111);
        }

        partial void OnMessageChanged(MessageModel value)
        {
            MessageCompleted = "OK";
        }
    }
}
