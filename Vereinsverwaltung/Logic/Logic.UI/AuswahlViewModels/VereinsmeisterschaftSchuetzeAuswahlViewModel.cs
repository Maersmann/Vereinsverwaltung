using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.AuswahlTypes;
using Data.Types.MitgliederTypes;
using System;
using System.Windows;

namespace Logic.UI.AuswahlViewModels
{
    public class VereinsmeisterschaftSchuetzeAuswahlViewModel : ViewModelAuswahl<SchuetzeModel, StammdatenTypes>
    {
        private AuswahlVereinsmeisterschaftSchuetzeTypes auswahltyp;
        private int vereinsmeisterschaftID;
        private Geschlecht? geschlecht;
        public VereinsmeisterschaftSchuetzeAuswahlViewModel()
        {
            auswahltyp = AuswahlVereinsmeisterschaftSchuetzeTypes.alle;
            Title = "Auswahl Schütze";
            RegisterAktualisereViewMessage(StammdatenTypes.schuetze.ToString());
        }

        protected override bool LoadingOnCreate() => false;
        public int? ID() => SelectedItem == null ? null : (int?)SelectedItem.ID;

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schuetze; }
        protected override string GetREST_API()
        {
            string SQL;
            SQL = auswahltyp switch
            {
                AuswahlVereinsmeisterschaftSchuetzeTypes.alle => $"/api/Schuetzen",
                AuswahlVereinsmeisterschaftSchuetzeTypes.nurFreieFuerVereinsmeisterschaft => $"/api/Schuetzen/Vereinsmeisterschaft/Frei?vereinsmeisterschaftId={vereinsmeisterschaftID}",
                _ => $"/api/VereinsmeisterschaftSchuetzen",
            };
            if (geschlecht.HasValue)
                SQL += SQL.Contains("?") ? $"&geschlecht={ (int)geschlecht}" : $" ?geschlecht={ (int)geschlecht}";

            return SQL;
        }
        protected override bool WithPagination() { return true; }

        public async void SetInfoForLoading(Geschlecht? geschlecht, int vereinsmeisterschaftID, AuswahlVereinsmeisterschaftSchuetzeTypes auswahlTyp)
        {
            this.vereinsmeisterschaftID = vereinsmeisterschaftID;
            this.geschlecht = geschlecht;
            auswahltyp = auswahlTyp;
            await LoadData();
        }

        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }
    }
}
