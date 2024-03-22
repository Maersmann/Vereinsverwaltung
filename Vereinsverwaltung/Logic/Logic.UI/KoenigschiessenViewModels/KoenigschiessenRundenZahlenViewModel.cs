using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Model.SchnurrschiessenModels;
using Data.Types;
using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenRundenZahlenViewModel : ViewModelBasis
    {
        private int jahr;
        private KoenigschiessenVarianten variante;
        private ObservableCollection<KoenigschiessenRundenZahlenDetailModel> detailList;
        private ObservableCollection<KoenigschiessenRundenZahlenModel> itemList;
        private KoenigschiessenRundenZahlenModel selectedItem;
        public KoenigschiessenRundenZahlenViewModel()
        {
            detailList = new ObservableCollection<KoenigschiessenRundenZahlenDetailModel>();
            itemList = new ObservableCollection<KoenigschiessenRundenZahlenModel>();
            Title = "Zahlen vom Königschiessen";
        }

        public async void ZeigeDatenAn(int jahr, KoenigschiessenVarianten variante)
        {
            this.variante = variante;
            this.jahr = jahr;
            await LoadData();
        }

        public async Task LoadData()
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/KoenigschiessenRunde/ZahlenVonRunden?jahr={jahr}&variante={variante}");
                if (resp.IsSuccessStatusCode)
                {
                    itemList = await resp.Content.ReadAsAsync<ObservableCollection<KoenigschiessenRundenZahlenModel>>();
                    OnPropertyChanged(nameof(ItemList));
                    if (itemList.Count > 0 )
                    {
                        SelectedItem = ItemList.First();
                    }
                }
            }
            RequestIsWorking = false;
        }

        #region Bindings
        public IEnumerable<KoenigschiessenRundenZahlenDetailModel> DetailList
        {
            get
            {
                return detailList;
            }
        }

        public ObservableCollection<KoenigschiessenRundenZahlenModel> ItemList => itemList;
        public KoenigschiessenRundenZahlenModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                if (SelectedItem != null)
                {
                    detailList = SelectedItem.Runden;
                }
                else
                {
                    detailList = new ObservableCollection<KoenigschiessenRundenZahlenDetailModel>();
                }

                OnPropertyChanged(nameof(DetailList));
            }
        }
        #endregion
    }
}
