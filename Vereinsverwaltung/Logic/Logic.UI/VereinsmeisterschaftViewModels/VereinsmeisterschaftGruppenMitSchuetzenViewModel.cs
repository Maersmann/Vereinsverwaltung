using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
     
    public class VereinsmeisterschaftGruppenMitSchuetzenViewModel : ViewModelUebersicht<VereinsmeisterschaftGruppenMitSchuetzenModel, StammdatenTypes>
    {
        private int vereinsmeisterschaftID;
        public VereinsmeisterschaftGruppenMitSchuetzenViewModel()
        {
            Title = "Übersicht Schützen";
        }
        public int VereinsmeisterschaftID 
        { 
            get => vereinsmeisterschaftID;
            set
            {
                vereinsmeisterschaftID = value;
                _ = LoadData();
            }
        }
        protected override bool LoadingOnCreate() => false;
        protected override string GetREST_API() { return $"/api/Schiessgruppen/Vereinsmeisterschaft/Schuetzen?vereinsmeisterschaftId={vereinsmeisterschaftID}"; }
        protected override bool WithPagination() { return true; }

    }
}
