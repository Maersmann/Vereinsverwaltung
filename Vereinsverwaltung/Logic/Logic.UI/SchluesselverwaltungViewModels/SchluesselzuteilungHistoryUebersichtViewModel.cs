using Data.Model.SchluesselverwaltungModels;
using Data.Types.SchluesselverwaltungTypes;
using Logic.Core;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselzuteilungHistoryUebersichtViewModel : ViewModelUebersicht<SchluesselzuteilungHistoryModel>
    {
        private SchluesselzuteilungTypes auswahlTypes;
        private int id;
        public SchluesselzuteilungHistoryUebersichtViewModel()
        {
            MessageToken = "SchluesselzuteilungHistoryUebersicht";
            Title = "History Verteilung";
        }

        public void SetAuswahlState(int id, SchluesselzuteilungTypes auswahlTypes)
        {
            this.auswahlTypes = auswahlTypes;
            this.id = id;
            LoadData();
        }

        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/history?id={id}&type={auswahlTypes}");
                if (resp.IsSuccessStatusCode)
                    itemList = await resp.Content.ReadAsAsync<ObservableCollection<SchluesselzuteilungHistoryModel>>();
            }
            base.LoadData();
        }
    }
}
