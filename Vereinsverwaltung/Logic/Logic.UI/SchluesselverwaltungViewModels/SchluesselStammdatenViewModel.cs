using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
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
using Base.Logic.Wrapper;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselStammdatenViewModel : ViewModelStammdaten<SchluesselModel, StammdatenTypes>, IViewModelStammdaten
    {
        public SchluesselStammdatenViewModel() 
        {
            Title = "Schlüssel Stammdaten";
        }

        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/schluessel/{id}");
                if (resp.IsSuccessStatusCode)
                    Response = await resp.Content.ReadAsAsync<Response<SchluesselModel>>();
            }

            Nummer = Data.Nummer;
            Beschreibung = Data.Beschreibung;
            Bezeichnung = Data.Bezeichnung;
            Bestand = Data.Bestand;
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
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/schluessel", Data);
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
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
            get => Data.Nummer;
            set
            {
                if (RequestIsWorking || !Equals(Data.Nummer, value))
                {
                    ValidateAnzahl(value, "Nummer");
                    Data.Nummer = value.GetValueOrDefault();
                    base.OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string Beschreibung
        {
            get => Data.Beschreibung;
            set
            {
                if (RequestIsWorking || !Equals(Data.Beschreibung, value))
                {
                    Data.Beschreibung = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string Bezeichnung
        {
            get => Data.Bezeichnung;
            set
            {
                if (RequestIsWorking || !Equals(Data.Bezeichnung, value))
                {
                    ValidateBezeichnung(value);
                    Data.Bezeichnung = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Bestand
        {
            get => Data.Bestand;
            set
            {
                if (RequestIsWorking || !Equals(Data.Bestand, value))
                {
                    Data.Bestand = value.GetValueOrDefault(0);
                    OnPropertyChanged();
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

        protected override void OnActivated()
        {
            Data = new SchluesselModel();
            Nummer = null;
            Bestand = null;
            Beschreibung = "";
            Bezeichnung = "";
            state = State.Neu;
        }

    }
}
