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
using Base.Logic.Wrapper;

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
                    Response<SchluesselModel> resData = await resp.Content.ReadAsAsync<Response<SchluesselModel>>();
                    Data.SchluesselID = resData.Data.ID;
                    Data.SchluesselBezeichnung = resData.Data.Bezeichnung;
                    auswahlTypes = SchluesselzuteilungTypes.Besitzer;
                    ((DelegateCommand)OpenAuswahlSchluesselCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)OpenAuswahlBesitzerCommand).RaiseCanExecuteChanged();
                    ValidateBezeichnung(Schluesselbesitzer, "Schluesselbesitzer", "Besitzer");
                    RaisePropertyChanged("SchluesselBez");
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
                    Response<SchluesselbesitzerModel> resData = await resp.Content.ReadAsAsync<Response<SchluesselbesitzerModel>>();
                    Data.SchluesselbesitzerID = resData.Data.ID;
                    Data.SchluesselbesitzerName = resData.Data.Name;
                    auswahlTypes = SchluesselzuteilungTypes.Schluessel;
                    ((DelegateCommand)OpenAuswahlSchluesselCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)OpenAuswahlBesitzerCommand).RaiseCanExecuteChanged();
                    ValidateBezeichnung(SchluesselBez, "SchluesselBez", "Schlüssel");
                    RaisePropertyChanged("Schluesselbesitzer");
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                RequestIsWorking = false;
            }
        }

        #region Bindings
        public string SchluesselBez => Data.SchluesselBezeichnung;
        public string Schluesselbesitzer => Data.SchluesselbesitzerName;

        public int? Anzahl
        {
            get => Data.Anzahl;
            set
            {
                if (!Equals(Data.Anzahl, value))
                {
                    ValidateAnzahl(value, "Anzahl");
                    Data.Anzahl = value.GetValueOrDefault(0);
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public DateTime? ErhaltenAm
        {
            get => Data.ErhaltenAm;
            set
            {

                if (!Equals(Data.ErhaltenAm, value))
                {
                    Data.ErhaltenAm = value.GetValueOrDefault(DateTime.Now);
                    ValidateEintrittsdatum(Data.ErhaltenAm);
                    base.RaisePropertyChanged();
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
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/new", Data);
                RequestIsWorking = false;
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if ((int)resp.StatusCode == 908)
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
            Messenger.Default.Send(new OpenSchluesselAuswahlMessage(OpenSchluesselAuswahlCallback), "SchluesselzuteilungStammdaten");
        }

        private void ExecuteOpenAuswahlBesitzerCommand()
        {
            Messenger.Default.Send(new OpenSchluesselbesitzerAuswahlMessage(OpenSchluesselbesitzerAuswahlCallback), "SchluesselzuteilungStammdaten");
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
                        Response<SchluesselbesitzerModel> resData = await resp.Content.ReadAsAsync<Response<SchluesselbesitzerModel>>();;
                        Data.SchluesselbesitzerID = resData.Data.ID;
                        Data.SchluesselbesitzerName = resData.Data.Name;
                        ValidateBezeichnung(Schluesselbesitzer, "Schluesselbesitzer", "Schlüssel");
                        RaisePropertyChanged("Schluesselbesitzer");
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
                        Response<SchluesselModel> resData = await resp.Content.ReadAsAsync<Response<SchluesselModel>>();
                        Data.SchluesselID = resData.Data.ID;
                        Data.SchluesselBezeichnung = resData.Data.Bezeichnung;
                        ValidateBezeichnung(SchluesselBez, "SchluesselBez", "Schlüssel");
                        RaisePropertyChanged("SchluesselBez");
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
            Data = new SchluesselzuteilungModel();
            ErhaltenAm = DateTime.Now;
            Anzahl = null;
            state = State.Neu;
            auswahlTypes = SchluesselzuteilungTypes.Undefiniert;
        }
    }
}
