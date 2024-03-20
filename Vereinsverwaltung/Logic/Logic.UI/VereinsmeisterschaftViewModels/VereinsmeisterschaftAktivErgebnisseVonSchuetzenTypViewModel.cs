using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.VereinsmeisterschaftTypes;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftAktivErgebnisseVonSchuetzenTypViewModel : ViewModelUebersicht<VereinsmeisterschaftschuetzeErgebnisModel, StammdatenTypes>
    {
        private int vereinsmeisterschaft;
        private VereinsmeisterschaftSchuetzeTyp typ;
        public VereinsmeisterschaftAktivErgebnisseVonSchuetzenTypViewModel()
        {
            vereinsmeisterschaft = 0;
            typ = VereinsmeisterschaftSchuetzeTyp.maennlich16_30;
            Title = "Übersicht Schützenergebnisse";
            WeakReferenceMessenger.Default.Register<LoadVereinsmeisterschaftAktivErgebnisseVonSchuetzentypMessage, string>(this, "VereinsmeisterschaftAktivErgebnisseSchuetzen", (r,m) => ReceiveLoadVereinsmeisterschaftAktivErgebnisseSchuetzenMessage(m));
        }

        private async void ReceiveLoadVereinsmeisterschaftAktivErgebnisseSchuetzenMessage(LoadVereinsmeisterschaftAktivErgebnisseVonSchuetzentypMessage m)
        {
            vereinsmeisterschaft = m.VereinsmeisterschaftID;
            typ = m.SchuetzeTyp;
            await LoadData();
        }

        protected override string GetREST_API() { return $"/api/VereinsmeisterschaftschuetzeErgebnisse/Vereinsmeisterschaft/Bereiche/Ergebnisse?vereinsmeisterschaftId={vereinsmeisterschaft}&typ={(int)typ}"; }
        protected override bool WithPagination() { return true; }
        protected override bool LoadingOnCreate() => false;      
    }
}
