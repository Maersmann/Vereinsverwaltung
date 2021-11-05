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
using System.Windows;

namespace Logic.UI.AuswahlViewModels
{
    public class PinAusgabeAuswahlViewModel : ViewModelAuswahl<PinAusgabeAuswahlModel, StammdatenTypes>
    {
        public PinAusgabeAuswahlViewModel()
        {
            Title = "Auswahl Pin Ausgabe";
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.pinAusgabe; }
        protected override string GetREST_API() { return $"/api/Pins/Ausgabe"; }
        protected override bool WithPagination() { return true; }
        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }

        #region Bindings
        public int? ID()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.ID;
        }
        #endregion
    }
}
