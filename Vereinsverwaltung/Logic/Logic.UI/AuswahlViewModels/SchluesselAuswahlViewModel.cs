using Data.Model.AuswahlModels;
using Data.Types;
using Base.Logic.ViewModels;

namespace Logic.UI.AuswahlViewModels
{
    public class SchluesselAuswahlViewModel : ViewModelAuswahl<SchluesselAuswahlModels, StammdatenTypes>
    {
        public SchluesselAuswahlViewModel()
        {
            Title = "Auswahl Schlüssel";
            LoadData();
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel.ToString());
        }

        public int? SchluesselID()
        {
            return selectedItem == null ? null : (int?)selectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluessel; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/schluessel"; }

    }
}
