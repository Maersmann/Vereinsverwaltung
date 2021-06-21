using Data.Model.SchnurrschiessenModels;
using Data.Types;
using Logic.Core;
using Logic.UI.BaseViewModels;
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
    public class SchnurUebersichtViewModel : ViewModelUebersicht<SchnurModel>
    {
        public SchnurUebersichtViewModel()
        {
            MessageToken = "SchnurUebersicht";
            Title = "Übersicht Schnüre";
            RegisterAktualisereViewMessage(StammdatenTypes.schnur);
        }

        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schnur; }

        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnur/sichtbar");
                if (resp2.IsSuccessStatusCode)
                    itemList = await resp2.Content.ReadAsAsync<ObservableCollection<SchnurModel>>();
            }
            base.LoadData();
        }

        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnur/{selectedItem.ID}");
                if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                    return;
                }
            }
            SendInformationMessage("Schnur gelöscht");
            base.ExecuteEntfernenCommand();
        }

        #endregion
    }
}
