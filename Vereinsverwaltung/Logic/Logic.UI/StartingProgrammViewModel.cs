using Data.Types;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;

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
            CheckServerIsOnline();
            if (GlobalVariables.ServerIsOnline)
            {
                ViewModelLocator locator = new ViewModelLocator();
                locator.Main.RaisePropertyChanged("MenuIsEnabled");
                Messenger.Default.Send<OpenViewMessage>(new OpenViewMessage { ViewType = ViewType.viewMitgliederUebersicht });
            }

            Messenger.Default.Send<CloseViewMessage>(new CloseViewMessage(), "StartingProgramm");
        }

        public ICommand CheckServerIsOnlineCommand { get; private set; }

        public void CheckServerIsOnline()
        {
            using TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("127.0.0.1", 5001);
                GlobalVariables.ServerIsOnline = true;
            }
            catch (Exception e)
            {
                GlobalVariables.ServerIsOnline = false;
                SendExceptionMessage("Backend ist nicht erreichbar" + Environment.NewLine + e.Message);
            }
        }
    }
}
