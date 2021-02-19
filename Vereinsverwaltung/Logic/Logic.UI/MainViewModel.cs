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
            OpenMitgliederStammdatenCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewMitlgiederStammdaten));
            OpenMitgliederUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewMitgliederUebersicht));
            OpenMitgliederImportCommand = new RelayCommand(() => ExecuteOpenViewCommand( ViewType.viewMitgliederImport));
        }

        public ICommand OpenConnectionCommand { get; private set; }
        public ICommand OpenMitgliederStammdatenCommand { get; private set; }
        public ICommand OpenMitgliederImportCommand { get; private set; }
        public RelayCommand OpenMitgliederUebersichtCommand { get; private set; }

        private void ExecuteOpenConnectionCommand()
        {
            var db = new DatabaseAPI();
            db.AktualisereDatenbank();
        }
        private void ExecuteOpenViewCommand(ViewType viewType)
        {
            Messenger.Default.Send<OpenViewMessage>(new OpenViewMessage { ViewType = viewType });
        }

    }
}