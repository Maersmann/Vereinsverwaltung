using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Base.Logic.ViewModels;
using System;
using System.Windows.Input;
using Data.Types;
using Logic.Messages.BaseMessages;
using Logic.Core;
using Logic.Core.OptionenLogic;
using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;

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
            NeuePinAusgabeCommand = new RelayCommand(() => ExecuteStammdatenViewCommand(StammdatenTypes.pinAusgabe));
            LadePinAusgabeCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewPinAusgabeUebersicht));
            AuswertungPinAusgabeTagCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungPinAusgabeTag));
            AuswertungPinAusgabeTagStundeCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungPinAusgabeTagStunde));           
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
        public ICommand LadePinAusgabeCommand { get; private set; }
        public ICommand NeuePinAusgabeCommand { get; private set; }
        public ICommand AuswertungPinAusgabeTagCommand { get; private set; }
        public ICommand AuswertungPinAusgabeTagStundeCommand { get; private set; }
        

        public bool MenuIsEnabled => GlobalVariables.ServerIsOnline;

        private void ExecuteOpenViewCommand(ViewType viewType)
        {
            Messenger.Default.Send(new OpenViewMessage { ViewType = viewType });
        }

        private void ExecuteStammdatenViewCommand(StammdatenTypes stammdaten)
        {
            Messenger.Default.Send(new BaseStammdatenMessage<StammdatenTypes> {Stammdaten  = stammdaten, State = State.Neu});
        }

        private void ExecuteOpenStartingViewCommand()
        {
            var backendlogic = new BackendLogic();
            if (!backendlogic.IstINIVorhanden())
            {
                Messenger.Default.Send<OpenKonfigurationViewMessage>(new OpenKonfigurationViewMessage { });
            }
            backendlogic.LoadData();
            GlobalVariables.BackendServer_IP = backendlogic.GetBackendIP();
            GlobalVariables.BackendServer_URL = backendlogic.GetURL();
            GlobalVariables.BackendServer_Port = backendlogic.GetBackendPort();

            Messenger.Default.Send<OpenStartingViewMessage>(new OpenStartingViewMessage { });
        }

    }
}