using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenErgebnisseVonMitgliedViewModel : ViewModelUebersicht<KoenigschiessenErgebnisseVonMitgliedModel, StammdatenTypes>
    {
        private int mitgliedID;
        public KoenigschiessenErgebnisseVonMitgliedViewModel()
        {
            Title = "Übersicht Ergebnisse Königschiessen";
        }

        protected override string GetREST_API() { return $"/api/Mitglieder/{mitgliedID}/Koenigschiessen";}
        protected override bool WithPagination() { return true; }
        protected override bool LoadingOnCreate() => false;

        public async void ZeigeDatenAn(int mitgliedID)
        {
            this.mitgliedID = mitgliedID;
            await LoadData();
        }

    }
}
