using Data.Model.PinModels;
using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Base.Logic.ViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using Base.Logic.Core;
using Base.Logic.Types;
using Base.Logic.Messages;
using Base.Logic.Wrapper;
using Logic.Messages.UtilMessages;

namespace Logic.UI.PinViewModels
{
    public class PinAusgabeStammdatenViewModel : ViewModelStammdaten<PinAusgabeModel, StammdatenTypes>, IViewModelStammdaten
    {
        private IList<PinModel> pins;
        public PinAusgabeStammdatenViewModel()
        {
            pins = [];
            Title = "Neue Pin Ausgabe";
            LoadArten();
        }

        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true; 
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins/Ausgabe/{id}");
                if (resp.IsSuccessStatusCode)
                    Response = await resp.Content.ReadAsAsync<Response<PinAusgabeModel>>();
            }

            Bezeichnung = Data.Bezeichnung;
            state = State.Bearbeiten;
            RequestIsWorking = false;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.pinAusgabe;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                Data.PinID = Data.Pin.ID;
                if (!Data.Option.NurAktive)
                {
                    Data.Option.Stichtag = null;
                }
                WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Liste wird erstellt." }, "PinAusgabeStammdaten");
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins/Ausgabe/new", Data);
                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.UnprocessableEntity))
                {
                    SendExceptionMessage("Pinausgabe konnte nicht erstellt werden." + Environment.NewLine + "Der Stichtag ist nicht ausgewählt");
                }
                else
                {
                    SendExceptionMessage("Pinausgabe konnte nicht erstellt werden.");
                }
                WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), "PinAusgabeStammdaten");
            }
        }
        #endregion

        #region Bindings

        public string Bezeichnung
        {
            get
            {
                return Data.Bezeichnung;
            }
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
        public DateTime? Stichtag
        {
            get { return Data.Option.Stichtag; }
            set
            {

                if (RequestIsWorking || !Equals(Data.Option.Stichtag, value))
                {
                    Data.Option.Stichtag = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public bool NurAktive
        {
            get { return Data.Option.NurAktive; }
            set
            {

                if (RequestIsWorking || !Equals(Data.Option.NurAktive, value))
                {
                    Data.Option.NurAktive = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Stichtag));
                }
            }
        }

        public IEnumerable<PinModel> Pins => pins;
        public PinModel Pin
        {
            get { return Data.Pin; }
            set
            {
                if (RequestIsWorking || (Data.Pin != value))
                {
                    Data.Pin = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Validierung

        private bool ValidateBezeichnung(string bezeichnung)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(bezeichnung, "", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Bezeichnung", validationErrors);
            return isValid;
        }
        #endregion

        private async void LoadArten()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp;
                resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins");

                if (resp.IsSuccessStatusCode)
                {
                    var Response = await resp.Content.ReadAsAsync<Response<ObservableCollection<PinModel>>>();
                    pins = Response.Data;
                }
                else
                    SendExceptionMessage("Pin-Arten konnten nicht gelade werden");
                
                RequestIsWorking = false;
            }

            OnPropertyChanged(nameof(Pins));
            if (pins.Count > 0)
            {
                Pin = pins.First();
            }
        }

        protected override void OnActivated()
        {
            Data = new PinAusgabeModel { Option = new PinAusgabeOptionModel(), Pin = new PinModel() };
            Bezeichnung = "";
            Stichtag = DateTime.Now;
            NurAktive = true;
            state = State.Neu;
        }
    }
}
