using Base.Logic.ViewModels;
using Data.Model.KkSchiessenModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.AuswahlViewModels
{
    public class KkSchiessgruppeAuswahlViewModel : ViewModelAuswahl<KkSchiessgruppeModel, StammdatenTypes>
    {
        public KkSchiessgruppeAuswahlViewModel()
        {
            Title = "Auswahl KK-Schießgruppe";
            LoadData();
            RegisterAktualisereViewMessage(StammdatenTypes.kkSchiessgruppe.ToString());
        }

        public int? ID()
        {
            return selectedItem == null ? null : (int?)selectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.kkSchiessgruppe; }
        protected override string GetREST_API() { return $"/api/KKSchiessGruppen"; }

    }
}
