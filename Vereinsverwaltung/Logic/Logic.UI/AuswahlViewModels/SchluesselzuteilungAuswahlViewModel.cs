using Data.Model.AuswahlModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using Logic.Core;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Logic.UI.AuswahlViewModels
{
    public class SchluesselzuteilungAuswahlViewModel : ViewModelAuswahl<SchluesselzuteilungAuswahlModel>
    {
        private SchluesselzuteilungTypes auswahlTypes;
        private int id;

        public SchluesselzuteilungAuswahlViewModel()
        {
            Title = "Auswahl Besitzer";
            LoadData();
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
        }

        public void SetAuswahlState(int id, SchluesselzuteilungTypes auswahlTypes)
        {
            this.auswahlTypes = auswahlTypes;
            this.id = id;
            LoadData();
        }

        public int? SchluesselzuteilungID()
        {
            if (selectedItem == null) return null;
            else return selectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluesselzuteilung; }

        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp;
                if (auswahlTypes.Equals(SchluesselzuteilungTypes.Besitzer))
                    resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/besitzer/{id}/schluessel");
                else
                    resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/schluessel/{id}/besitzer");

                if (resp.IsSuccessStatusCode)
                    itemList = await resp.Content.ReadAsAsync<ObservableCollection<SchluesselzuteilungAuswahlModel>>();
            }
            base.LoadData();
        }

    }
}
