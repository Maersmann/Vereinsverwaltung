using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Vereinsverwaltung.Data.Entitys.MitgliederEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Core.MitgliederCore;
using Vereinsverwaltung.Logic.Messages.AuswahlMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.AuswahlViewModels
{
    public class MitgliedAuswahlViewModel : ViewModelAuswahl<Mitglied>
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
            itemList = new MitgliedAPI().LadeAlleAktiven();
            base.LoadData();
        }

        protected override bool OnFilterTriggered(object item)
        {
            if (item is Mitglied mitglied)
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
