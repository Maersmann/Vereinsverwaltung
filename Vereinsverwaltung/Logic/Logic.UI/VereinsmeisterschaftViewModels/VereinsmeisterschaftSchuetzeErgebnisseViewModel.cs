using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftSchuetzeErgebnisseViewModel : ViewModelUebersicht<VereinsmeisterschaftschuetzeErgebnisModel, StammdatenTypes>
    {
        private int schuetzeID;
        public VereinsmeisterschaftSchuetzeErgebnisseViewModel()
        {
            Title = "Ergebnisse vom Schützen";
        }
        public int SchuetzeID
        {
            get => schuetzeID;
            set
            {
                schuetzeID = value;
                _ = LoadData();
            }
        }
        protected override bool LoadingOnCreate() => false;
        protected override string GetREST_API() { return $"/api/VereinsmeisterschaftschuetzeErgebnisse/Schuetze?schuetzeID={schuetzeID}"; }
        protected override bool WithPagination() { return true; }

    }
}
