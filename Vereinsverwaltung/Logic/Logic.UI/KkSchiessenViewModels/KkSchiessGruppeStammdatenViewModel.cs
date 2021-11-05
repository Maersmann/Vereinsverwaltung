using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.KkSchiessenModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Logic.UI.KkSchiessenViewModels
{
    public class KkSchiessGruppeStammdatenViewModel : ViewModelStammdaten<KkSchiessgruppeModel, StammdatenTypes>, IViewModelStammdaten
    {
        public KkSchiessGruppeStammdatenViewModel()
        {
            Title = "Stammdaten KK-Schießgruppe";
        }

        public async void ZeigeStammdatenAn(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/KKSchiessGruppen/{id}");
                if (resp.IsSuccessStatusCode)
                    Response = await resp.Content.ReadAsAsync<Response<KkSchiessgruppeModel>>();
            }
            Name = Data.Name;
            state = State.Bearbeiten;
            RequestIsWorking = false;
        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.kkSchiessgruppe;
        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/KKSchiessGruppen", Data);
                RequestIsWorking = false;
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else
                {
                    SendExceptionMessage("KK-Schießgruppe konnte nicht gespeichert werden.");
                }
            }
        }
        #endregion

        #region Bindings
        public string Name
        {
            get => Data.Name;
            set
            {
                if (RequestIsWorking || !Equals(Data.Name, value))
                {
                    ValidateName(value);
                    Data.Name = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Validierung

        private bool ValidateName(string bezeichnung)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(bezeichnung, "", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Name", validationErrors);
            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            Data = new KkSchiessgruppeModel();
            Name = "";
            state = State.Neu;
        }
    }
}
