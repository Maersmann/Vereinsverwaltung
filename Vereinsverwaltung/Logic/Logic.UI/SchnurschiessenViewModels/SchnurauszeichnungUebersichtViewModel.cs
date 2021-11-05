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
    public class SchnurauszeichnungUebersichtViewModel : ViewModelUebersicht<SchnurauszeichnungModel, StammdatenTypes>
    {
        private bool canNew;
        public SchnurauszeichnungUebersichtViewModel()
        {
            canNew = false;
            MessageToken = "SchnurauszeichnungUebersicht";
            Title = "Übersicht Schnurauszeichnungen";
            NeuCommand = new DelegateCommand(ExecuteNeuCommand, CanExecuteNeuCommand);
            RegisterAktualisereViewMessage(StammdatenTypes.schnurauszeichnung.ToString());
            RegisterAktualisereViewMessage(StammdatenTypes.schnur.ToString());
            CheckCanExecuteNeuCommand();
        }

        protected override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            CheckCanExecuteNeuCommand();
            base.ReceiveAktualisiereViewMessage(m);
        }
        protected override int GetID() { return SelectedItem.ID; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schnurauszeichnung; }
        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/schnurschiessen/Schnurauszeichnung"; }


        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung/{SelectedItem.ID}");
                if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Auszeichnung konnte nicht gelöscht werden.");
                    RequestIsWorking = false;
                    return;
                }
            }
            SendInformationMessage("Auszeichnung gelöscht");
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
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung/CanNew");
                if (resp.IsSuccessStatusCode)
                    canNew = await resp.Content.ReadAsAsync<bool>();
                RequestIsWorking = false;
            }
            ((DelegateCommand)NeuCommand).RaiseCanExecuteChanged();
        }
    }
}
