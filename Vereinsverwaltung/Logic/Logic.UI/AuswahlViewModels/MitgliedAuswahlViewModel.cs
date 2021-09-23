using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Data.Types;
using Base.Logic.ViewModels;
using Data.Model.AuswahlModels;
using Logic.Core;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace Logic.UI.AuswahlViewModels
{
    public class MitgliedAuswahlViewModel : ViewModelAuswahl<MitgliedAuswahlModel, StammdatenTypes>
    {
        private string filtertext;

        public MitgliedAuswahlViewModel()
        {
            Title = "Auswahl Mitglied";
            filtertext = "";
            LoadData();
            RegisterAktualisereViewMessage(StammdatenTypes.mitglied.ToString());
        }

        public int? MitgliedID() 
        {
            return selectedItem == null ? null : (int?)selectedItem.ID;
        }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.mitglied; }
        protected override string GetREST_API() { return $"/api/Mitglieder"; }

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
        #endregion
    }
}
