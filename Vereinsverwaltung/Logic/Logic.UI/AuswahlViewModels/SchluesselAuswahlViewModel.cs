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
    public class SchluesselAuswahlViewModel : ViewModelAuswahl<SchluesselAuswahlModels>
    {
        public SchluesselAuswahlViewModel()
        {
            Title = "Auswahl Schlüssel";
            LoadData();
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel);
        }

        public int? SchluesselID()
        {
            if (selectedItem == null) return null;
            else return selectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluessel; }

        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/schluessel");
                if (resp2.IsSuccessStatusCode)
                    itemList = await resp2.Content.ReadAsAsync<ObservableCollection<SchluesselAuswahlModels>>();
            }
            base.LoadData();
        }
    }
}
