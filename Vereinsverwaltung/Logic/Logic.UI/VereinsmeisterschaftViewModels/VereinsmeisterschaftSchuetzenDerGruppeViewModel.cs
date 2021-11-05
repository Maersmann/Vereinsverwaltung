using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftSchuetzenDerGruppeViewModel : ViewModelUebersicht<VereinsmeisterschaftSchuetzenDerGruppeModel, StammdatenTypes>
    {
        private int schiessgruppeID;
        public VereinsmeisterschaftSchuetzenDerGruppeViewModel()
        {
            Title = "Übersicht Schützen";
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
        protected override string GetREST_API() { return $"/api/Schiessgruppen/{schiessgruppeID}/Vereinsmeisterschaft/Schuetzen"; }
        protected override bool WithPagination() { return true; }

    }
}
