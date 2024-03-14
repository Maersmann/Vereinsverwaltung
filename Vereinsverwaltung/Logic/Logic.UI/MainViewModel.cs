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
using System.Collections.Generic;
using System.Windows.Controls;
using Logic.Messages.UserMessages;

namespace Logic.UI
{
    public class MainViewModel : ViewModelBasis
    {
        public MainViewModel()
        {
            Title = "Vereinsverwaltung";

            GlobalVariables.ServerIsOnline = false;
            GlobalVariables.BackendServer_URL = "";
            GlobalVariables.Token = "";
            BerechtigungenService.Berechtigungen = new List<BerechtigungTypes>();
            BerechtigungenService.IsAdmin = false;
            GlobalUserVariables.UserID = 0;

            AbmeldenCommand = new RelayCommand(() => ExecuteAbmeldenCommand());
            PasswordAendernCommand = new RelayCommand(() => ExecutePasswordAendernCommand());
            OpenStartingViewCommand = new RelayCommand(() => ExecuteOpenStartingViewCommand());
            OpenMitgliederUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewMitgliederUebersicht));
            OpenMitgliederImportCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewMitgliederImport));
            OpenSchluesselUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchluesselUebersicht));
            OpenSchluesselbesitzerUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchluesselbesitzerUebersicht));
            OpenZuteilungSchluesselbesitzerUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewZuteilungSchluesselbesitzerUebersicht));
            OpenZuteilungSchluesselUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewZuteilungSchluesselUebersicht));
            OpenZuteilungFreieAnzahlUbersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewZuteilungFreieAnzahlUebersicht));
            NeuePinAusgabeCommand = new RelayCommand(() => ExecuteStammdatenViewCommand(StammdatenTypes.pinAusgabe));
            LadePinAusgabeCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewPinAusgabeUebersicht));
            AuswertungPinAusgabeTagCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungPinAusgabeTag));
            AuswertungPinAusgabeTagStundeCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungPinAusgabeTagStunde));
            ExportSchluesselCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewExportSchluessel));
            ExportMitgliederAenderungenCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewExportMitgliederAenderungen));
            KkSchiessenUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewKkSchiessenUebersicht));
            KkSchiessgruppeUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewKkSchiessgruppeUebersicht));
            AuswertungKkSchiessenMonatCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungKkSchiessenMonat));
            AuswertungKkSchiessenMonatJahresvergleichCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungKkSchiessenMonatJahresvergleich));
            VereinsmeisterschaftOffenUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewVereinsmeisterschaftAktiveVereinsmeisterschaft));
            SchuetzenUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchuetzenUebersicht));
            SchiessgruppenUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchiessgruppenUebersicht));
            ExportVereinsmeisterschaftCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewExportVereinsmeisterschaft));
            VereinsmeisterschaftAktivErgebnisseSchuetzenCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewVereinsmeisterschaftAktiveVereinsmeisterschaftErgebnisseSchuetzen));
            VereinsmeisterschaftAktivErgebnisseGruppenCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewVereinsmeisterschaftAktiveVereinsmeisterschaftErgebnisseGruppen));
            VereinsmeisterschaftenUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewVereinsmeisterschaftenUebersicht));
            AuswertungVereinsmeisterschaftEntwicklungGruppenCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungVereinsmeisterschaftEntwicklungGruppen));
            AuswertungVereinsmeisterschaftEntwicklungSchuetzenCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungVereinsmeisterschaftEntwicklungSchuetzen));
            AuswertungMitgliederAuswertungEintrittCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungMitgliederAuswertungEintritt));
            AuswertungMitgliederAuswertungJahreImVereinCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungMitgliederAuswertungJahreImVerein));
            AuswertungMitgliederAuswertungJahrgangCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungMitgliederAuswertungJahrgang));
            AuswertungMitgliederAuswertungJahrzehntCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAuswertungMitgliederAuswertungJahrzehnt));
            KoenigschiessenUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewKoenigschiessenUebersicht));
            KoenigschiessenErstellenCommand = new RelayCommand(() => ExecuteStammdatenViewCommand(StammdatenTypes.koenigschiessen));
            JugendkoenigschiessenUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewJugendkoenigschiessenUebersicht));
            JugendkoenigschiessenErstellenCommand = new RelayCommand(() => ExecuteStammdatenViewCommand(StammdatenTypes.jugendkoenigschiessen));
            SchnurschiessenMitgliederUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenMitgliederUebersicht));
            SchnurBestandHistorieCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenAuszeichnungBestandHistorie));
            AktiveSchnurschiessenVerwaltungCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAktiveSchnurschiessenVerwaltung));
            AktivesSchnurschiessenMitgliederUebersichtCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewAktivesSchnurschiessenMitgliederUebersicht));
            SchnurschiessenMitgliederImportCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenMitgliederImport));
            SchnurschiessenAuswertungAktuellenStandAuszeichnungCommmand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenAuswertungAktuellenStandAuszeichnung));
            SchnurschiessenAuswertungAktuellenStandRangCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenAuswertungAktuellenStandRang));
            SchnurschiessenAuswertungEntwicklungAuszeichnungCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenAuswertungEntwicklungAuszeichnung));
            SchnurschiessenAuswertungEntwicklungRangCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenAuswertungEntwicklungRang));
            SchnurschiessenAuswertungGesamtteilnahmeCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenAuswertungGesamtteilnahme));
            SchnurschiessenAuswertungErhalteneAuszeichnungCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenAuswertungErhalteneAuszeichnung));
            SchnurschiessenAuswertungNeuerRangCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenAuswertungNeuerRang));
            SchnurschiessenAuswertungTeilnahmeProTagCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenAuswertungTeilnahmeProTag));
            SchnurschiessenMitgliederZuordnungCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewSchnurschiessenMitgliederZuordnung));
            ExportSchnurschiessenCommand = new RelayCommand(() => ExecuteOpenViewCommand(ViewType.viewExportSchnurschiessen));
            
        }

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
        public ICommand ExportSchluesselCommand { get; private set; }
        public ICommand ExportMitgliederAenderungenCommand { get; private set; }
        public ICommand KkSchiessenUebersichtCommand { get; private set; }
        public ICommand KkSchiessgruppeUebersichtCommand { get; private set; }
        public ICommand AuswertungKkSchiessenMonatCommand { get; private set; }
        public ICommand AuswertungKkSchiessenMonatJahresvergleichCommand { get; set; }
        public ICommand VereinsmeisterschaftOffenUebersichtCommand { get; set; }
        public ICommand SchuetzenUebersichtCommand { get; set; }
        public ICommand SchiessgruppenUebersichtCommand { get; set; }
        public ICommand ExportVereinsmeisterschaftCommand { get; set; }
        public ICommand VereinsmeisterschaftAktivErgebnisseSchuetzenCommand { get; set; }
        public ICommand VereinsmeisterschaftAktivErgebnisseGruppenCommand { get; set; }
        public ICommand VereinsmeisterschaftenUebersichtCommand { get; set; }
        public ICommand AuswertungVereinsmeisterschaftEntwicklungSchuetzenCommand { get; set; }
        public ICommand AuswertungVereinsmeisterschaftEntwicklungGruppenCommand { get; set; }
        public ICommand AuswertungMitgliederAuswertungEintrittCommand { get; set; }
        public ICommand AuswertungMitgliederAuswertungJahreImVereinCommand { get; set; }
        public ICommand AuswertungMitgliederAuswertungJahrgangCommand { get; set; }
        public ICommand AuswertungMitgliederAuswertungJahrzehntCommand { get; set; }
        public ICommand KoenigschiessenUebersichtCommand { get; set; }
        public ICommand KoenigschiessenErstellenCommand { get; set; }
        public ICommand JugendkoenigschiessenUebersichtCommand { get; set; }
        public ICommand JugendkoenigschiessenErstellenCommand { get; set; }
        public ICommand SchnurschiessenMitgliederUebersichtCommand { get; set; }
        public ICommand SchnurBestandHistorieCommand { get; set; }
        public ICommand AktiveSchnurschiessenVerwaltungCommand { get; set; }
        public ICommand AktivesSchnurschiessenMitgliederUebersichtCommand { get; set; }
        public ICommand SchnurschiessenMitgliederImportCommand { get; set; }
        public ICommand SchnurschiessenAuswertungAktuellenStandAuszeichnungCommmand { get; set; }
        public ICommand SchnurschiessenAuswertungAktuellenStandRangCommand { get; set; }
        public ICommand SchnurschiessenAuswertungEntwicklungAuszeichnungCommand { get; set; }
        public ICommand SchnurschiessenAuswertungEntwicklungRangCommand { get; set; }
        public ICommand SchnurschiessenAuswertungGesamtteilnahmeCommand {get;set;}
        public ICommand SchnurschiessenAuswertungErhalteneAuszeichnungCommand { get; set; }
        public ICommand SchnurschiessenAuswertungNeuerRangCommand { get; set; }
        public ICommand SchnurschiessenAuswertungTeilnahmeProTagCommand { get; set; }
        public ICommand SchnurschiessenMitgliederZuordnungCommand { get; set; }
        public ICommand ExportSchnurschiessenCommand {  get; set; }




        public bool MenuIsEnabled => GlobalVariables.ServerIsOnline;
        
        public ICommand AbmeldenCommand { get; set; }
        public ICommand PasswordAendernCommand { get; set; }

        


        public RelayCommand<PasswordBox> PasswordCommand { get; private set; }

        private void ExecuteOpenViewCommand(ViewType viewType)
        {
            Messenger.Default.Send(new OpenViewMessage { ViewType = viewType });
        }

        private void ExecuteStammdatenViewCommand(StammdatenTypes stammdaten)
        {
            Messenger.Default.Send(new BaseStammdatenMessage<StammdatenTypes> {Stammdaten  = stammdaten, State = State.Neu});
        }

        private void ExecuteAbmeldenCommand()
        {
            GlobalVariables.Token = "";
            BerechtigungenService.Berechtigungen = new List<BerechtigungTypes>();
            BerechtigungenService.IsAdmin = false;
            GlobalUserVariables.UserID = 0;
            Messenger.Default.Send(new AktualisiereBerechtigungenMessage { });
            Messenger.Default.Send(new OpenViewMessage { ViewType = ViewType.viewNothing });
            Messenger.Default.Send(new OpenLoginViewMessage { });
        }


        private void ExecutePasswordAendernCommand()
        {
            Messenger.Default.Send(new OpenPasswordAendernViewMessage { });
        }

        private void ExecuteOpenStartingViewCommand()
        {
            var backendlogic = new BackendLogic();
            if (!backendlogic.IstINIVorhanden())
            {
                Messenger.Default.Send(new OpenKonfigurationViewMessage { });
            }
            backendlogic.LoadData();
            GlobalVariables.BackendServer_IP = backendlogic.GetBackendIP();
            GlobalVariables.BackendServer_URL = backendlogic.GetURL();
            GlobalVariables.BackendServer_Port = backendlogic.GetBackendPort();

            Messenger.Default.Send(new OpenStartingViewMessage { });
        }

        protected override void ReceiveOpenViewMessage()
        {
            RaisePropertyChanged("MenuIsEnabled");
            base.ReceiveOpenViewMessage();
        }

    }
}