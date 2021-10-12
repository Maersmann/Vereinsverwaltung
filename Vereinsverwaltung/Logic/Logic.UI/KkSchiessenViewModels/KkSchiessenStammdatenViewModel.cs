using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Data.Model.KkSchiessenModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.KkSchiessenViewModels
{
    public class KkSchiessenStammdatenViewModel : ViewModelStammdaten<KkSchiessenModel, StammdatenTypes>, IViewModelStammdaten
    {
        private bool gruppeAusgewaehlt;
        public KkSchiessenStammdatenViewModel()
        {
            Title = "Stammdaten KK-Schießen";
            OpenAuswahlCommand = new RelayCommand(() => ExecuteOpenAuswahlCommand());
        }
        public async void ZeigeStammdatenAn(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/KkSchiessen/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    data = await resp.Content.ReadAsAsync<KkSchiessenModel>();
                }
            }
            state = State.Bearbeiten;
            gruppeAusgewaehlt = true;
            Datum = data.Datum;
            Getraenke = data.Getraenke;
            Munition = data.PackungenMunition;
            RaisePropertyChanged(nameof(GruppenBezeichnung));
            RequestIsWorking = false;
        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.kkSchiessen;
        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                data.Datum = data.Datum.Add(DateTime.Now.TimeOfDay);
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/KkSchiessen", data);
                RequestIsWorking = false;
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else
                {
                    SendExceptionMessage("KK-Schießen konnte nicht gespeichert werden.");
                }
            }
        }

        private void ExecuteOpenAuswahlCommand()
        {
            Messenger.Default.Send(new OpenKkSchiessgruppeAuswahlMessage(OpenKkSchiessgruppeAuswahlMessageCallback), "KkSchiessenStammdaten");
        }

        protected override bool CanExecuteSaveCommand()
        {
            return base.CanExecuteSaveCommand() && gruppeAusgewaehlt;
        }
        #endregion

        #region Callback
        private async void OpenKkSchiessgruppeAuswahlMessageCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                RequestIsWorking = true;
                if (GlobalVariables.ServerIsOnline)
                {
                    HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/KKSchiessGruppen/{id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        KkSchiessgruppeModel resData = await resp.Content.ReadAsAsync<KkSchiessgruppeModel>();
                        data.KkSchiessGruppeID = resData.ID;
                        data.KkSchiessGruppe = resData;
                        RaisePropertyChanged(nameof(GruppenBezeichnung));
                        gruppeAusgewaehlt = true;
                        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    }
                }
                RequestIsWorking = false;
            }
        }
        #endregion

        #region Bindings
        public int? Getraenke
        {
            get => data.Getraenke;
            set
            {
                if (RequestIsWorking || !Equals(data.Getraenke, value))
                {
                    data.Getraenke = value.GetValueOrDefault(0);
                    ValidateAnzahl(data.Getraenke, nameof(Getraenke)); 
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Munition
        {
            get => data.PackungenMunition;
            set
            {
                if (RequestIsWorking || !Equals(data.PackungenMunition, value))
                {
                    data.PackungenMunition = value.GetValueOrDefault(0);
                    ValidateAnzahl(data.PackungenMunition, nameof(Munition));
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime? Datum
        {
            get => data.Datum;
            set
            {

                if (!Equals(data.Datum, value))
                {
                    data.Datum = value.GetValueOrDefault(DateTime.Now);
                    ValidateDatum(data.Datum);
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand OpenAuswahlCommand { get; set; }

        public string GruppenBezeichnung => data.KkSchiessGruppe.Name;
        #endregion

        #region Validierung

        private bool ValidateAnzahl(int? anzahl, string fieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateIntegerAllow0(anzahl, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }
        private bool ValidateDatum(DateTime? datum)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateDatum(datum, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Datum", validationErrors);

            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            data = new KkSchiessenModel { KkSchiessGruppe = new KkSchiessgruppeModel() };
            Getraenke = null;
            Munition = null;
            Datum = DateTime.Now;
            state = State.Neu;
            gruppeAusgewaehlt = false;
        }
    }
}
