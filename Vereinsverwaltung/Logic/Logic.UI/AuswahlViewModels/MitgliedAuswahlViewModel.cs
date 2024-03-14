using Data.Types;
using Base.Logic.ViewModels;
using Data.Model.AuswahlModels;
using System.Windows;

namespace Logic.UI.AuswahlViewModels
{
    public class MitgliedAuswahlViewModel : ViewModelAuswahl<MitgliedAuswahlModel, StammdatenTypes>
    {
        private bool nurOhneSchnurschiessenRang;
        public MitgliedAuswahlViewModel()
        {
            nurOhneSchnurschiessenRang = false;
            Title = "Auswahl Mitglied";
            RegisterAktualisereViewMessage(StammdatenTypes.mitglied.ToString());
        }

        public async void NurOhneSchnurschiessenRang()
        {
            nurOhneSchnurschiessenRang = true;
            await LoadData();
        }

        public int? ID()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.ID;
        }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.mitglied; }
        protected override string GetREST_API() { return nurOhneSchnurschiessenRang ? "/api/Mitglieder/OhneSchnurschiessenRang" : $"/api/Mitglieder"; }
        protected override bool WithPagination() { return true; }
        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }
    }
}
