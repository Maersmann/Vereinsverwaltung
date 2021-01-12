using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Vereinsverwaltung.Logic.Core;
using Vereinsverwaltung.Logic.UI.BaseViewModels;
using System;
using System.Windows.Input;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Data.Types;

namespace Vereinsverwaltung.Logic.UI
{
    public class MainViewModel : ViewModelBasis
    {
        public MainViewModel()
        {
            OpenConnectionCommand = new RelayCommand(() => ExecuteOpenConnectionCommand());
            OpenMitgliederStammdatenCommand = new RelayCommand(() => ExecuteOpenMitgliederStammdatenCommand());
        }

        
        public ICommand OpenConnectionCommand { get; private set; }
        public ICommand OpenMitgliederStammdatenCommand { get; private set; }



        private void ExecuteOpenConnectionCommand()
        {
            var db = new DatabaseAPI();
            db.AktualisereDatenbank();
        }
        private void ExecuteOpenMitgliederStammdatenCommand()
        {
            Messenger.Default.Send<OpenViewMessage>(new OpenViewMessage { ViewType = ViewType.viewMitlgiederStammdaten });
        }

    }
}