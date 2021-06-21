using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Data.Types;
using Logic.UI.BaseViewModels;
using Data.Model.AuswahlModels;
using Logic.Core;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace Logic.UI.AuswahlViewModels
{
    public class MitgliedAuswahlViewModel : ViewModelAuswahl<MitgliedAuswahlModel>
    {
        private string filtertext;

        public MitgliedAuswahlViewModel()
        {
            Title = "Auswahl Mitglied";
            filtertext = "";
            LoadData();
            RegisterAktualisereViewMessage(StammdatenTypes.mitglied);
        }

        public int? MitgliedID() 
        {
            if (selectedItem == null) return null;
            else return selectedItem.ID;
        }


        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.mitglied; }

        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder");
                if (resp2.IsSuccessStatusCode)
                    itemList = await resp2.Content.ReadAsAsync<ObservableCollection<MitgliedAuswahlModel>>();
            }
            base.LoadData();
        }

        protected override bool OnFilterTriggered(object item)
        {
            if (item is MitgliedAuswahlModel mitglied)
            {
                var MitgliedsNr = Convert.ToString(mitglied.Mitgliedsnr);
                return mitglied.Fullname.Contains(filtertext) || MitgliedsNr.Contains(filtertext);
            }
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
        #endregion
    }
}
