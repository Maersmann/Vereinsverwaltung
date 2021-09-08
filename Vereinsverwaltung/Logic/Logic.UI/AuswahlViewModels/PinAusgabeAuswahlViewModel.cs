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


        public override async void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Pins/Ausgabe");
                if (resp.IsSuccessStatusCode)
                {
                    itemList = await resp.Content.ReadAsAsync<ObservableCollection<PinAusgabeAuswahlModel>>();
                }
            }
            base.LoadData();
        }

        protected override bool OnFilterTriggered(object item)
        {
            return true;
        }

        #region Bindings
        public string FilterText
        {
            get => filtertext;
            set
            {
                filtertext = value;
                RaisePropertyChanged();
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
