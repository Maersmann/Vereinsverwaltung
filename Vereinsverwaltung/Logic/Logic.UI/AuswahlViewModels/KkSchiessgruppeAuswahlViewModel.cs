using Base.Logic.ViewModels;
using Data.Model.KkSchiessenModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Logic.UI.AuswahlViewModels
{
    public class KkSchiessgruppeAuswahlViewModel : ViewModelAuswahl<KkSchiessgruppeModel, StammdatenTypes>
    {
        public KkSchiessgruppeAuswahlViewModel()
        {
            Title = "Auswahl KK-Schießgruppe";
            RegisterAktualisereViewMessage(StammdatenTypes.kkSchiessgruppe.ToString());
        }

        public int? ID()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.ID;
        }

        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.kkSchiessgruppe; }
        protected override string GetREST_API() { return $"/api/KKSchiessGruppen"; }
        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }

    }
}
