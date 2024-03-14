using Data.Model.SchnurrschiessenModels;
using Data.Types;
using Data.Types.SchnurschiessenTypes;
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
using Base.Logic.Wrapper;
using System.Windows.Input;
using System.ComponentModel.Design;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenAuszeichnungStammdatenViewModel : ViewModelStammdaten<SchnurschiessenAuszeichnungModel, StammdatenTypes>, IViewModelStammdaten
    {

        public SchnurschiessenAuszeichnungStammdatenViewModel()
        {
            Title = "Auszeichnung Stammdaten";
        }

        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Schnurschiessenauszeichnung/{id}");
                if (resp.IsSuccessStatusCode)
                    Response = await resp.Content.ReadAsAsync<Response<SchnurschiessenAuszeichnungModel>>();
            }
            Bezeichnung = Data.Bezeichnung;
            state = State.Bearbeiten;
            RequestIsWorking = false;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnurschiessenAuszeichnung;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Schnurschiessenauszeichnung", Data);
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Auszeichznung konnte nicht gespeichert werden.");
                }
            }           
        }
        #endregion

        #region Bindings


        public string Bezeichnung
        {
            get => Data.Bezeichnung;
            set
            {
                if (RequestIsWorking || !Equals(Data.Bezeichnung, value))
                {
                    ValidateBezeichnung(value);
                    Data.Bezeichnung = value;
                    base.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
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

        public override void Cleanup()
        {
            Data = new SchnurschiessenAuszeichnungModel {};
            Bezeichnung = "";
            state = State.Neu;
        }
    }
}
