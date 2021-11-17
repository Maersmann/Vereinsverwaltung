using Data.Types;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Base.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using Base.Logic.Core;

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
            Messenger.Default.Send(new CloseViewMessage(), "StartingProgramm");
            if (GlobalVariables.ServerIsOnline)
            {
                Messenger.Default.Send(new OpenLoginViewMessage { });
            }
            else
            {
                Messenger.Default.Send(new CloseApplicationMessage { });
            }
        }

        public ICommand CheckServerIsOnlineCommand { get; private set; }

    }
}
