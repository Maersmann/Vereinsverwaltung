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

        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung");
                if (resp.IsSuccessStatusCode)
                    itemList = await resp.Content.ReadAsAsync<ObservableCollection<SchnurauszeichnungModel>>();
            }
            base.LoadData();
        }

        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung/{selectedItem.ID}");
                if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
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
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung/CanNew");
                if (resp2.IsSuccessStatusCode)
                    canNew = await resp2.Content.ReadAsAsync<bool>();
            }
            ((DelegateCommand)NeuCommand).RaiseCanExecuteChanged();
        }
    }
}
