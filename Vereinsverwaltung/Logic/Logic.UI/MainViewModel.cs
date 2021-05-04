using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.UI.BaseViewModels;
using System;
using System.Windows.Input;
using Data.Types;
using Logic.Messages.BaseMessages;
using Logic.Core;

namespace Logic.UI
{
    public class MainViewModel : ViewModelBasis
    {
        public MainViewModel()
        {
            Title = "Vereinsverwaltung";
            GlobalVariables.ServerIsOnline = false;
            OpenStartingViewCommand = new RelayCommand(() => ExecuteOpenStartingViewCommand());
            OpenMitgliederStammdatenCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewMitlgiederStammdaten));
            OpenMitgliederUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewMitgliederUebersicht));
            OpenMitgliederImportCommand = new RelayCommand(() => ExecuteOpenViewCommand( ViewType.viewMitgliederImport));
            OpenSchluesselUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchluesselUebersicht));
            OpenSchluesselbesitzerUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchluesselbesitzerUebersicht));
            OpenZuteilungSchluesselbesitzerUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewZuteilungSchluesselbesitzerUebersicht));
            OpenZuteilungSchluesselUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewZuteilungSchluesselUebersicht));
            OpenZuteilungFreieAnzahlUbersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewZuteilungFreieAnzahlUebersicht));
        }

        public ICommand OpenMitgliederStammdatenCommand { get; private set; }
        public ICommand OpenMitgliederImportCommand { get; private set; }
        public ICommand OpenMitgliederUebersichtCommand { get; private set; }
        public ICommand OpenSchluesselUebersichtCommand { get; private set; }
        public ICommand OpenSchluesselbesitzerUebersichtCommand { get; private set; }
        public ICommand OpenZuteilungSchluesselbesitzerUebersichtCommand { get; private set; }
        public ICommand OpenZuteilungSchluesselUebersichtCommand { get; private set; }
        public ICommand OpenZuteilungFreieAnzahlUbersichtCommand { get; private set; }
        public ICommand OpenStartingViewCommand { get; private set; }

        public bool MenuIsEnabled => GlobalVariables.ServerIsOnline;

        private void ExecuteOpenViewCommand(ViewType viewType)
        {
            Messenger.Default.Send<OpenViewMessage>(new OpenViewMessage { ViewType = viewType });
        }

        private void ExecuteOpenStartingViewCommand()
        {
            Messenger.Default.Send<OpenStartingViewMessage>(new OpenStartingViewMessage { });
        }

    }
}