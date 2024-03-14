using Base.Logic.ViewModels;
using Data.Model.AuswahlModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Logic.UI.AuswahlViewModels
{
    public class SchnurschiessenRangAuswahlViewModel : ViewModelAuswahl<SchnurschiessenRangAuswahlModel, StammdatenTypes>
    {
        public SchnurschiessenRangAuswahlViewModel()
        {
            Title = "Auswahl Rang";
            RegisterAktualisereViewMessage(StammdatenTypes.schnurschiessenRang.ToString());
        }

        public int? ID()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schnurschiessenRang; }
        protected override string GetREST_API() { return $"/api/Schnurschiessenrang"; }
        protected override bool WithPagination() { return true; }
        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }

    }
}
