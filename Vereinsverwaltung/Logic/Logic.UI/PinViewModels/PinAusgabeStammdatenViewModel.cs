using Data.Model.PinModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.UI.BaseViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Logic.UI.PinViewModels
{
    public class PinAusgabeStammdatenViewModel : ViewModelStammdaten<PinAusgabeModel>, IViewModelStammdaten
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
            LoadData = true; 
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins/Ausgabe/{id}");
                if (resp.IsSuccessStatusCode)
                    data = await resp.Content.ReadAsAsync<PinAusgabeModel>();
            }

            Bezeichnung = data.Bezeichnung;
            state = State.Bearbeiten;
            LoadData = false;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.pinAusgabe;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                data.PinID = data.Pin.ID;
                if (!data.Option.NurAktive)
                {
                    data.Option.Stichtag = null;
                }

                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins/Ausgabe/new", data);

                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), GetStammdatenTyp());
                }
                else
                {
                    SendExceptionMessage("Pin konnte nicht gespeichert werden.");
                    return;
                }

            }
        }
        #endregion

        #region Bindings

        public String Bezeichnung
        {
            get
            {
                return data.Bezeichnung;
            }
            set
            {
                if (LoadData || !string.Equals(data.Bezeichnung, value))
                {
                    ValidateBezeichnung(value);
                    data.Bezeichnung = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public DateTime? Stichtag
        {
            get { return data.Option.Stichtag; }
            set
            {

                if (LoadData || !string.Equals(data.Option.Stichtag, value))
                {
                    data.Option.Stichtag = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public Boolean NurAktive
        {
            get { return data.Option.NurAktive; }
            set
            {

                if (LoadData || !string.Equals(data.Option.NurAktive, value))
                {
                    data.Option.NurAktive = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged(nameof(Stichtag));
                }
            }
        }

        public IEnumerable<PinModel> Pins => pins;
        public PinModel Pin
        {
            get { return data.Pin; }
            set
            {
                if (LoadData || (this.data.Pin != value))
                {
                    this.data.Pin = value;
                    this.RaisePropertyChanged();
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
                HttpResponseMessage resp;
                resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins");

                if (resp.IsSuccessStatusCode)
                    pins = await resp.Content.ReadAsAsync<ObservableCollection<PinModel>>();
                else
                    SendExceptionMessage("Pin-Arten konnten nicht gelade werden");
            }

            this.RaisePropertyChanged(nameof(Pins));
            if (pins.Count > 0)
                Pin = pins.First();
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
