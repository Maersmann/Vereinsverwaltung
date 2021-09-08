using Data.Model.AuswahlModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.UI.BaseViewModels;
using System.Net.Http;
using Logic.Core;
using System.Collections.ObjectModel;

namespace Logic.UI.AuswahlViewModels
{
    public class SchluesselbesitzerAuswahlViewModel : ViewModelAuswahl<SchluesselbesitzerAuswahlModel>
    {
        public SchluesselbesitzerAuswahlViewModel()
        {
            Title = "Auswahl Besitzer";
            LoadData();
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselbesitzer);
        }

        public int? SchluesselbestizerID()
        {
            if (selectedItem == null) return null;
            else return selectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluesselbesitzer; }

        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer");
                if (resp.IsSuccessStatusCode)
                    itemList = await resp.Content.ReadAsAsync<ObservableCollection<SchluesselbesitzerAuswahlModel>>();
            }
            base.LoadData();
        }
    }
}
