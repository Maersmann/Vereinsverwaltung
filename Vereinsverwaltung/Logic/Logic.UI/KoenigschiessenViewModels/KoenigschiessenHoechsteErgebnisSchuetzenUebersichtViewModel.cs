using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Types;
using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewModel : ViewModelUebersicht<KoenigschiessenHoechsteErgebnisSchuetzenUebersichtModel, StammdatenTypes>
    {
        private int jahr;
        private int runde;
        private KoenigschiessenArt art;
        public KoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewModel()
        {
            Title = "Übersicht Schützen - Beste Ergebnisse";
        }

        protected override string GetREST_API() { return $"/api/KoenigschiessenRunde/SchuetzenMitHoechstenErgebnis?jahr={jahr}&runde={runde}&art={art}"; }
        protected override bool WithPagination() { return true; }
        protected override bool LoadingOnCreate() => false;

        public async void ZeigeDatenAn(int jahr, int runde, KoenigschiessenArt art)
        {
            this.art = art;
            this.jahr = jahr;
            this.runde = runde;
            await LoadData();
        }

    }
}
