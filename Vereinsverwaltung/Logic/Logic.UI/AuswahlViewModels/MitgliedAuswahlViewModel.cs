using Data.Types;
using Base.Logic.ViewModels;
using Data.Model.AuswahlModels;
using System.Windows;

namespace Logic.UI.AuswahlViewModels
{
    public class MitgliedAuswahlViewModel : ViewModelAuswahl<MitgliedAuswahlModel, StammdatenTypes>
    {

        public MitgliedAuswahlViewModel()
        {
            Title = "Auswahl Mitglied";
            RegisterAktualisereViewMessage(StammdatenTypes.mitglied.ToString());
        }

        public int? ID()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.ID;
        }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.mitglied; }
        protected override string GetREST_API() { return $"/api/Mitglieder"; }
        protected override bool WithPagination() { return true; }
        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }
    }
}
