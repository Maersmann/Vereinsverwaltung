using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Base.Logic.ViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Base.Logic.Core;
using Base.Logic.Types;
using Base.Logic.Messages;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselStammdatenViewModel : ViewModelStammdaten<SchluesselModel, StammdatenTypes>, IViewModelStammdaten
    {
        public SchluesselStammdatenViewModel() 
        {
            Title = "Schlüssel Stammdaten";
        }

        public async void ZeigeStammdatenAn(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/schluessel/{id}");
                if (resp.IsSuccessStatusCode)
                    data = await resp.Content.ReadAsAsync<SchluesselModel>();
            }

            Nummer = data.Nummer;
            Beschreibung = data.Beschreibung;
            Bezeichnung = data.Bezeichnung;
            Bestand = data.Bestand;
            state = State.Bearbeiten;
            RequestIsWorking = false;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schluessel;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/schluessel", data);
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if ((int)resp.StatusCode == 906)
                {
                    SendExceptionMessage("Nummer ist schon vorhanden");
                }
                else
                {
                    SendExceptionMessage("Schlüssel konnte nicht gespeichert werden.");
                }
            }
        }
        #endregion

        #region Bindings
        public int? Nummer
        {
            get => data.Nummer;
            set
            {
                if (RequestIsWorking || !Equals(data.Nummer, value))
                {
                    ValidateAnzahl(value, "Nummer");
                    data.Nummer = value.GetValueOrDefault();
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string Beschreibung
        {
            get => data.Beschreibung;
            set
            {
                if (RequestIsWorking || !Equals(data.Beschreibung, value))
                {
                    data.Beschreibung = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public string Bezeichnung
        {
            get => data.Bezeichnung;
            set
            {
                if (RequestIsWorking || !Equals(data.Bezeichnung, value))
                {
                    ValidateBezeichnung(value);
                    data.Bezeichnung = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Bestand
        {
            get => data.Bestand;
            set
            {
                if (RequestIsWorking || !Equals(data.Bestand, value))
                {
                    data.Bestand = value.GetValueOrDefault(0);
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Validierung
        private bool ValidateAnzahl(int? anzahl, string fieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateInteger(anzahl, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }
        private bool ValidateBezeichnung(string bezeichnung)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(bezeichnung, "", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Bezeichnung", validationErrors);
            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            data = new SchluesselModel();
            Nummer = null;
            Bestand = null;
            Beschreibung = "";
            Bezeichnung = "";
            state = State.Neu;
        }

    }
}
