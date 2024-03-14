using Data.Model.SchnurrschiessenModels;
using Data.Types;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Base.Logic.ViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Base.Logic.Messages;
using Base.Logic.Core;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenrangUebersichtViewModel : ViewModelUebersicht<SchnurschiessenrangModel, StammdatenTypes>
    {
        private bool canNew;
        public SchnurschiessenrangUebersichtViewModel()
        {
            canNew = false;
            MessageToken = "SchnurschiessenrangUebersicht";
            Title = "Übersicht Ränge";
            NeuCommand = new DelegateCommand(ExecuteNeuCommand, CanExecuteNeuCommand);
            RegisterAktualisereViewMessage(StammdatenTypes.schnurschiessenRang.ToString());
            RegisterAktualisereViewMessage(StammdatenTypes.schnurschiessenAuszeichnung.ToString());
            CheckCanExecuteNeuCommand();
        }

        protected override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            CheckCanExecuteNeuCommand();
            base.ReceiveAktualisiereViewMessage(m);
        }
        protected override int GetID() { return SelectedItem.ID; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schnurschiessenRang; }
        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/Schnurschiessenrang"; }


        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/Schnurschiessenrang/{SelectedItem.ID}");
                if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                    RequestIsWorking = false;
                    return;
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Rang konnte nicht gelöscht werden.");
                    RequestIsWorking = false;
                    return;
                }
            }
            SendInformationMessage("Rang gelöscht");
            base.ExecuteEntfernenCommand();
            RequestIsWorking = false;
        }

        private bool CanExecuteNeuCommand()
        {
            return canNew;
        }

        #endregion

        private async void CheckCanExecuteNeuCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Schnurschiessenrang/EintragVorhanden");
                if (resp.IsSuccessStatusCode)
                    canNew = await resp.Content.ReadAsAsync<bool>();
                RequestIsWorking = false;
            }
            ((DelegateCommand)NeuCommand).RaiseCanExecuteChanged();
        }
    }
}
