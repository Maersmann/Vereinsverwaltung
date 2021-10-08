using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.BaseMessages;
using Base.Logic.ViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselzuteilungStammdatenViewModel : ViewModelStammdaten<SchluesselzuteilungModel, StammdatenTypes>, ISchluesselzuteilungStammdatenViewModel
    {
        private SchluesselzuteilungTypes auswahlTypes;

        public SchluesselzuteilungStammdatenViewModel() : base()
        {
            Title = "Schlüsselzuteilung";
            OpenAuswahlSchluesselCommand = new DelegateCommand(this.ExecuteOpenAuswahlSchluesselCommand, this.CanExecuteOpenAuswahlSchluesselCommand);
            OpenAuswahlBesitzerCommand = new DelegateCommand(this.ExecuteOpenAuswahlBesitzerCommand, this.CanOpenAuswahlBesitzerCommand);
            Cleanup();
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schluesselzuteilung;

        public async void BySchluesselID(int schluesselID)
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/schluessel/{schluesselID}");
                if (resp.IsSuccessStatusCode)
                {
                    var resData = await resp.Content.ReadAsAsync<SchluesselModel>();
                    data.SchluesselID = resData.ID;
                    data.SchluesselBezeichnung = resData.Bezeichnung;
                    auswahlTypes = SchluesselzuteilungTypes.Besitzer;
                    ((DelegateCommand)OpenAuswahlSchluesselCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)OpenAuswahlBesitzerCommand).RaiseCanExecuteChanged();
                    ValidateBezeichnung(Schluesselbesitzer, "Schluesselbesitzer", "Besitzer");
                    this.RaisePropertyChanged("SchluesselBez");
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                RequestIsWorking = false;
            }
        }

        public async void BySchluesselbesitzerID(int schluesselbesitzerID)
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer/{schluesselbesitzerID}");
                if (resp.IsSuccessStatusCode)
                {
                    var resData = await resp.Content.ReadAsAsync<SchluesselbesitzerModel>();
                    data.SchluesselbesitzerID = resData.ID;
                    data.SchluesselbesitzerName = resData.Name;
                    auswahlTypes = SchluesselzuteilungTypes.Schluessel;
                    ((DelegateCommand)OpenAuswahlSchluesselCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)OpenAuswahlBesitzerCommand).RaiseCanExecuteChanged();
                    ValidateBezeichnung(SchluesselBez, "SchluesselBez", "Schlüssel");
                    this.RaisePropertyChanged("Schluesselbesitzer");
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                RequestIsWorking = false;
            }
        }

        #region Bindings
        public String SchluesselBez
        {
            get
            {
                 return data.SchluesselBezeichnung;
            }
        }
        public String Schluesselbesitzer
        {
            get
            {
                return data.SchluesselbesitzerName; 
            }
        }

        public int? Anzahl
        {
            get => data.Anzahl;
            set
            {
                if (!Equals(data.Anzahl, value))
                {
                    ValidateAnzahl(value, "Anzahl");
                    data.Anzahl = value.GetValueOrDefault(0);
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public DateTime? ErhaltenAm
        {
            get => data.ErhaltenAm;
            set
            {

                if (!Equals(data.ErhaltenAm, value))
                {
                    data.ErhaltenAm = value.GetValueOrDefault(DateTime.Now);
                    ValidateEintrittsdatum(data.ErhaltenAm);
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand OpenAuswahlSchluesselCommand { get; set; }
        public ICommand OpenAuswahlBesitzerCommand { get; set; }
        #endregion

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/new", data);
                RequestIsWorking = false;
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if ((int)resp.StatusCode == 902)
                {
                    SendExceptionMessage("Es sind zu wenig Schlüssel frei");
                }
                else
                {
                    SendExceptionMessage("Schlüsselzuteilung konnte nicht gespeichert werden.");
                }
                

            }
        }
        private bool CanOpenAuswahlBesitzerCommand()
        {
            return auswahlTypes == SchluesselzuteilungTypes.Besitzer;
        }

        private bool CanExecuteOpenAuswahlSchluesselCommand()
        {
            return auswahlTypes == SchluesselzuteilungTypes.Schluessel;
        }

        private void ExecuteOpenAuswahlSchluesselCommand()
        {
            Messenger.Default.Send<OpenSchluesselAuswahlMessage>(new OpenSchluesselAuswahlMessage(OpenSchluesselAuswahlCallback), "SchluesselzuteilungStammdaten");
        }

        private void ExecuteOpenAuswahlBesitzerCommand()
        {
            Messenger.Default.Send<OpenSchluesselbesitzerAuswahlMessage>(new OpenSchluesselbesitzerAuswahlMessage(OpenSchluesselbesitzerAuswahlCallback), "SchluesselzuteilungStammdaten");
        }
        #endregion

        #region Callback
        private async void OpenSchluesselbesitzerAuswahlCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    RequestIsWorking = true;
                    HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer/{id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        var resData = await resp.Content.ReadAsAsync<SchluesselbesitzerModel>();;
                        data.SchluesselbesitzerID = resData.ID;
                        data.SchluesselbesitzerName = resData.Name;
                        ValidateBezeichnung(Schluesselbesitzer, "Schluesselbesitzer", "Schlüssel");
                        this.RaisePropertyChanged("Schluesselbesitzer");
                        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    }
                    RequestIsWorking = false;
                }
            }
        }
        private async void OpenSchluesselAuswahlCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    RequestIsWorking = true;
                    HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/schluessel/{id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        var resData = await resp.Content.ReadAsAsync<SchluesselModel>();
                        data.SchluesselID = resData.ID;
                        data.SchluesselBezeichnung = resData.Bezeichnung;
                        ValidateBezeichnung(SchluesselBez, "SchluesselBez", "Schlüssel");
                        this.RaisePropertyChanged("SchluesselBez");
                        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    }
                    RequestIsWorking = false;
                }
            }
        }
        #endregion

        #region Validierung
        private bool ValidateBezeichnung(string bezeichnung, string fieldname, string messagefieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(bezeichnung, messagefieldname, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }

        private bool ValidateAnzahl(int? anzahl, string fieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateInteger(anzahl, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }
        private bool ValidateEintrittsdatum(DateTime? eintrittsdatum)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateDatum(eintrittsdatum, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "ErhaltenAm", validationErrors);

            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            data = new SchluesselzuteilungModel();
            ErhaltenAm = DateTime.Now;
            Anzahl = null;
            state = State.Neu;
            auswahlTypes = SchluesselzuteilungTypes.Undefiniert;
        }
    }
}
