using Data.Model.AuswahlModels;
using Data.Model.PinModels;
using Logic.Core;
using Base.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Data.Types;

namespace Logic.UI.AuswahlViewModels
{
    public class PinAusgabeAuswahlViewModel : ViewModelAuswahl<PinAusgabeAuswahlModel, StammdatenTypes>
    {
        private string filtertext;

        public PinAusgabeAuswahlViewModel()
        {
            Title = "Auswahl Pin Ausgabe";
            filtertext = "";
            LoadData();
        }

        protected override string GetREST_API() { return $"/api/Pins/Ausgabe"; }

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
