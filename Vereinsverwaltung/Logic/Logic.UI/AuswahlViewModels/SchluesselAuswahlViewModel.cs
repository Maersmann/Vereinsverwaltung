using Data.Model.AuswahlModels;
using Data.Types;
using Base.Logic.ViewModels;
using System.Windows;

namespace Logic.UI.AuswahlViewModels
{
    public class SchluesselAuswahlViewModel : ViewModelAuswahl<SchluesselAuswahlModels, StammdatenTypes>
    {
        public SchluesselAuswahlViewModel()
        {
            Title = "Auswahl Schlüssel";
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel.ToString());
        }

        public int? ID()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluessel; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/schluessel"; }
        protected override bool WithPagination() { return true; }
        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }

    }
}
