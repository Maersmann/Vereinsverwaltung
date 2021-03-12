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
            OpenSchluesselUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchluesselUebersicht));
            OpenSchluesselbesitzerUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchluesselbesitzerUebersicht));
            OpenZuteilungSchluesselbesitzerUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewZuteilungSchluesselbesitzerUebersicht));
            OpenZuteilungSchluesselUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewZuteilungSchluesselUebersicht));
            OpenZuteilungFreieAnzahlUbersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewZuteilungFreieAnzahlUebersicht));
        }

        public ICommand OpenConnectionCommand { get; private set; }
        public ICommand OpenMitgliederStammdatenCommand { get; private set; }
        public ICommand OpenMitgliederImportCommand { get; private set; }
        public ICommand OpenMitgliederUebersichtCommand { get; private set; }
        public ICommand OpenSchluesselUebersichtCommand { get; private set; }
        public ICommand OpenSchluesselbesitzerUebersichtCommand { get; private set; }
        public ICommand OpenZuteilungSchluesselbesitzerUebersichtCommand { get; private set; }
        public ICommand OpenZuteilungSchluesselUebersichtCommand { get; private set; }

        public ICommand OpenZuteilungFreieAnzahlUbersichtCommand { get; private set; }
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