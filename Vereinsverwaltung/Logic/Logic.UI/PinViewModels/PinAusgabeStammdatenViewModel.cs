using Data.Model.PinModels;
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Base.Logic.Core;
using Base.Logic.Types;
using Base.Logic.Messages;

namespace Logic.UI.PinViewModels
{
    public class PinAusgabeStammdatenViewModel : ViewModelStammdaten<PinAusgabeModel, StammdatenTypes>, IViewModelStammdaten
    {
        private IList<PinModel> pins;
        public PinAusgabeStammdatenViewModel()
        {
            Cleanup();
            pins = new List<PinModel>();
            Title = "Neue Pin Ausgabe";
            LoadArten();
        }

        public async void ZeigeStammdatenAn(int id)
        {
            RequestIsWorking = true; 
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins/Ausgabe/{id}");
                if (resp.IsSuccessStatusCode)
                    data = await resp.Content.ReadAsAsync<PinAusgabeModel>();
            }

            Bezeichnung = data.Bezeichnung;
            state = State.Bearbeiten;
            RequestIsWorking = false;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.pinAusgabe;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                data.PinID = data.Pin.ID;
                if (!data.Option.NurAktive)
                {
                    data.Option.Stichtag = null;
                }

                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins/Ausgabe/new", data);
                RequestIsWorking = false;
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else
                {
                    SendExceptionMessage("Pin konnte nicht gespeichert werden.");
                }
            }
        }
        #endregion

        #region Bindings

        public string Bezeichnung
        {
            get
            {
                return data.Bezeichnung;
            }
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
        public DateTime? Stichtag
        {
            get { return data.Option.Stichtag; }
            set
            {

                if (RequestIsWorking || !Equals(data.Option.Stichtag, value))
                {
                    data.Option.Stichtag = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public bool NurAktive
        {
            get { return data.Option.NurAktive; }
            set
            {

                if (RequestIsWorking || !Equals(data.Option.NurAktive, value))
                {
                    data.Option.NurAktive = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(Stichtag));
                }
            }
        }

        public IEnumerable<PinModel> Pins => pins;
        public PinModel Pin
        {
            get { return data.Pin; }
            set
            {
                if (RequestIsWorking || (data.Pin != value))
                {
                    data.Pin = value;
                    RaisePropertyChanged();
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
                    pins = await resp.Content.ReadAsAsync<ObservableCollection<PinModel>>();
                }
                else
                    SendExceptionMessage("Pin-Arten konnten nicht gelade werden");
                
                RequestIsWorking = false;
            }

            RaisePropertyChanged(nameof(Pins));
            if (pins.Count > 0)
            {
                Pin = pins.First();
            }
        }

        public override void Cleanup()
        {
            data = new PinAusgabeModel { Option = new PinAusgabeOptionModel(), Pin = new PinModel() };
            Bezeichnung = "";
            Stichtag = DateTime.Now;
            NurAktive = true;
            state = State.Neu;
        }
    }
}
