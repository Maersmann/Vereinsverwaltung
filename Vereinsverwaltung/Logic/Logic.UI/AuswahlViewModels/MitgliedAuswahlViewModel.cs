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

        public override void LoadData()
        {
            // Todo: Request
            /*
            itemList = new MitgliedAPI().LadeAlleAktiven();
            base.LoadData();
            */
        }

        protected override bool OnFilterTriggered(object item)
        {
            // Todo: Request
            /*
            if (item is Mitglied mitglied)
            {
                var MitgliedsNr = Convert.ToString(mitglied.Mitgliedsnr);
                return mitglied.Fullname.Contains(filtertext) || MitgliedsNr.Contains(filtertext);
            }
            */
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
