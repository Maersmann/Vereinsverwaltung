using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftGruppeErgebnisseViewModel : ViewModelUebersicht<VereinsmeisterschaftGruppeErgebnisModel, StammdatenTypes>
    {
        private int schiessgruppeID;
        public VereinsmeisterschaftGruppeErgebnisseViewModel()
        {
            Title = "Ergebnisse der Gruppe";
        }
        public int SchiessgruppeID
        {
            get => schiessgruppeID;
            set
            {
                schiessgruppeID = value;
                _ = LoadData();
            }
        }
        protected override bool LoadingOnCreate() => false;
        protected override string GetREST_API() { return $"/api/VereinmeisterschaftGruppeErgebnisse/Gruppe?gruppeID={schiessgruppeID}"; }
        protected override bool WithPagination() { return true; }

    }
}
