using Data.Model.SchnurrschiessenModels;
using Data.Types;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Logic.UI.BaseViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurauszeichnungUebersichtViewModel : ViewModelUebersicht<SchnurauszeichnungModel>
    {
        private bool canNew;
        public SchnurauszeichnungUebersichtViewModel()
        {
            canNew = false;
            MessageToken = "SchnurauszeichnungUebersicht";
            Title = "Übersicht Schnurauszeichnungen";
            NeuCommand = new DelegateCommand(this.ExecuteNeuCommand, this.CanExecuteNeuCommand);
            RegisterAktualisereViewMessage(StammdatenTypes.schnurauszeichnung);
            RegisterAktualisereViewMessage(StammdatenTypes.schnur);
            CheckCanExecuteNeuCommand();
        }

        protected override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            CheckCanExecuteNeuCommand();
            base.ReceiveAktualisiereViewMessage(m);
        }
        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schnurauszeichnung; }
        protected override string GetREST_API() { return $"/api/schnurschiessen/Schnurauszeichnung"; }


        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung/{selectedItem.ID}");
                if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Auszeichnung konnte nicht gelöscht werden.");
                    return;
                }
            }
            SendInformationMessage("Auszeichnung gelöscht");
            base.ExecuteEntfernenCommand();
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
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung/CanNew");
                if (resp.IsSuccessStatusCode)
                    canNew = await resp.Content.ReadAsAsync<bool>();
            }
            ((DelegateCommand)NeuCommand).RaiseCanExecuteChanged();
        }
    }
}
