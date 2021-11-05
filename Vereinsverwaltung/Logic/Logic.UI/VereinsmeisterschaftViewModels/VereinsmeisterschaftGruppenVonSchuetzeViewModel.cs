using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftGruppenVonSchuetzeViewModel : ViewModelUebersicht<VereinsmeisterschaftGruppenVonSchuetzeModel, StammdatenTypes>
    {
        private int schuetzeID;
        public VereinsmeisterschaftGruppenVonSchuetzeViewModel()
        {
            Title = "Übersicht Gruppen";
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
        protected override string GetREST_API() { return $"/api/Schuetzen/{schuetzeID}/Vereinsmeisterschaft/Gruppen"; }
        protected override bool WithPagination() { return true; }

    }
}
