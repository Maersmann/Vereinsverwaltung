using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
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
        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/KkSchiessen/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    Response = await resp.Content.ReadAsAsync<Response<KkSchiessenModel>>();
                }
            }
            state = State.Bearbeiten;
            gruppeAusgewaehlt = true;
            Datum = Data.Datum;
            Getraenke = Data.Getraenke;
            Munition = Data.PackungenMunition;
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
                Data.Datum = Data.Datum.Add(DateTime.Now.TimeOfDay);
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/KkSchiessen", Data);
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
                        Response<KkSchiessgruppeModel> resData = await resp.Content.ReadAsAsync<Response<KkSchiessgruppeModel>>();
                        Data.KkSchiessGruppeID = resData.Data.ID;
                        Data.KkSchiessGruppe = resData.Data;
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
            get => Data.Getraenke;
            set
            {
                if (RequestIsWorking || !Equals(Data.Getraenke, value))
                {
                    Data.Getraenke = value.GetValueOrDefault(0);
                    ValidateAnzahl(Data.Getraenke, nameof(Getraenke)); 
                    base.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Munition
        {
            get => Data.PackungenMunition;
            set
            {
                if (RequestIsWorking || !Equals(Data.PackungenMunition, value))
                {
                    Data.PackungenMunition = value.GetValueOrDefault(0);
                    ValidateAnzahl(Data.PackungenMunition, nameof(Munition));
                    base.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime? Datum
        {
            get => Data.Datum;
            set
            {

                if (!Equals(Data.Datum, value))
                {
                    Data.Datum = value.GetValueOrDefault(DateTime.Now);
                    ValidateDatum(Data.Datum);
                    base.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand OpenAuswahlCommand { get; set; }

        public string GruppenBezeichnung => Data.KkSchiessGruppe.Name;
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
            Data = new KkSchiessenModel { KkSchiessGruppe = new KkSchiessgruppeModel() };
            Getraenke = null;
            Munition = null;
            Datum = DateTime.Now;
            state = State.Neu;
            gruppeAusgewaehlt = false;
        }
    }
}
