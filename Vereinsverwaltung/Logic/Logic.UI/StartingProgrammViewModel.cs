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
            RequestIsWorking = true;

            new BackendHelper().CheckServerIsOnline();
            if (GlobalVariables.ServerIsOnline)
            {
                ViewModelLocator locator = new ViewModelLocator();
                locator.Main.RaisePropertyChanged("MenuIsEnabled");
                Messenger.Default.Send(new OpenViewMessage { ViewType = ViewType.viewMitgliederUebersicht });
            }

            Messenger.Default.Send(new CloseViewMessage(), "StartingProgramm");
        }

        public ICommand CheckServerIsOnlineCommand { get; private set; }

    }
}
