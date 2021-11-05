using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.VereinsmeisterschaftTypes;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftAktivErgebnisseVonGruppenTypViewModel : ViewModelUebersicht<VereinsmeisterschaftGruppeErgebnisModel, StammdatenTypes>
    {
        private int vereinsmeisterschaft;
        private VereinsmeisterschaftGruppeTyp typ;
        public VereinsmeisterschaftAktivErgebnisseVonGruppenTypViewModel()
        {
            vereinsmeisterschaft = 0;
            typ = VereinsmeisterschaftGruppeTyp.maennlich;
            Title = "Übersicht Schützenergebnisse";
            Messenger.Default.Register<LoadVereinsmeisterschaftAktivErgebnisseVonGruppentypMessage>(this, "VereinsmeisterschaftAktivErgebnisseGruppen", m => ReceiveLoadVereinsmeisterschaftAktivErgebnisseGruppenMessage(m));
        }

        private async void ReceiveLoadVereinsmeisterschaftAktivErgebnisseGruppenMessage(LoadVereinsmeisterschaftAktivErgebnisseVonGruppentypMessage m)
        {
            vereinsmeisterschaft = m.VereinsmeisterschaftID;
            typ = m.GruppeTyp;
            await LoadData();
        }

        protected override string GetREST_API() { return $"/api/VereinmeisterschaftGruppeErgebnisse/Vereinsmeisterschaft/Bereiche/Ergebnisse?vereinsmeisterschaftId={vereinsmeisterschaft}&typ={(int)typ}"; }
        protected override bool WithPagination() { return true; }
        protected override bool LoadingOnCreate() => false;
    }
}
