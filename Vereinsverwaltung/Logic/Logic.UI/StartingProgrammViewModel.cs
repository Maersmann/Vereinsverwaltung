using Data.Types;

using CommunityToolkit.Mvvm.Messaging;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Base.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using Base.Logic.Core;
using CommunityToolkit.Mvvm.Input;

namespace Logic.UI
{
    public class StartingProgrammViewModel : ViewModelBasis
    {
        public StartingProgrammViewModel()
        {
            Title = "Loading...";
            CheckServerIsOnlineCommand = new RelayCommand(() => ExecuteCheckServerIsOnlineCommand());
        }

        private void ExecuteCheckServerIsOnlineCommand()
        {
            new BackendHelper().CheckServerIsOnline();
            WeakReferenceMessenger.Default.Send(new CloseViewMessage(), "StartingProgramm");
            if (GlobalVariables.ServerIsOnline)
            {
                WeakReferenceMessenger.Default.Send(new OpenLoginViewMessage { });
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new CloseApplicationMessage { });
            }
        }

        public ICommand CheckServerIsOnlineCommand { get; private set; }

    }
}
