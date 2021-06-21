using Data.Model.AuswahlModels;
using Data.Model.PinModels;
using Logic.Core;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace Logic.UI.AuswahlViewModels
{
    public class PinAusgabeAuswahlViewModel : ViewModelAuswahl<PinAusgabeAuswahlModel>
    {
        private string filtertext;

        public PinAusgabeAuswahlViewModel()
        {
            Title = "Auswahl Pin Ausgabe";
            filtertext = "";
            LoadData();
        }


        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Pins/Ausgabe");
                if (resp2.IsSuccessStatusCode)
                    itemList = await resp2.Content.ReadAsAsync<ObservableCollection<PinAusgabeAuswahlModel>>();
            }
            base.LoadData();
        }

        protected override bool OnFilterTriggered(object item)
        {
            return true;
        }

        #region Bindings
        public String FilterText
        {
            get => this.filtertext;
            set
            {
                this.filtertext = value;
                this.RaisePropertyChanged();
                _customerView.Refresh();
            }
        }
        public int? ID()
        {
            if (selectedItem == null) return null;
            else return selectedItem.ID;
        }
        #endregion
    }
}
