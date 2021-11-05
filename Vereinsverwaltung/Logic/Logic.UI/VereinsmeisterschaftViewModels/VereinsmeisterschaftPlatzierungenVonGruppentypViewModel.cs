using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.VereinsmeisterschaftTypes;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftPlatzierungenVonGruppentypViewModel : ViewModelUebersicht<VereinsmeisterschaftGruppeErgebnisModel, StammdatenTypes>
    {
        private int vereinsmeisterschaft;
        private VereinsmeisterschaftGruppeTyp typ;
        public VereinsmeisterschaftPlatzierungenVonGruppentypViewModel()
        {
            vereinsmeisterschaft = 0;
            typ = VereinsmeisterschaftGruppeTyp.maennlich;
            Title = "Übersicht Schützenplatzierungen";
            Messenger.Default.Register<LoadVereinsmeisterschaftPlatzierungenVonGruppentypMessage>(this, "VereinsmeisterschaftPlatzierungenGruppenBereich", m => ReceiveLoadVereinsmeisterschaftPlatzierungenGruppenMessage(m));
        }

        private async void ReceiveLoadVereinsmeisterschaftPlatzierungenGruppenMessage(LoadVereinsmeisterschaftPlatzierungenVonGruppentypMessage m)
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
