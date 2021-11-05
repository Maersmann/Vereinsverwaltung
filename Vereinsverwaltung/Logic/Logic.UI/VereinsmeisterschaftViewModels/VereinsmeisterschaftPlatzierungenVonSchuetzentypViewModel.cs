using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.VereinsmeisterschaftTypes;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftPlatzierungenVonSchuetzentypViewModel : ViewModelUebersicht<VereinsmeisterschaftschuetzeErgebnisModel, StammdatenTypes>
    {
        private int vereinsmeisterschaft;
        private VereinsmeisterschaftSchuetzeTyp typ;
        public VereinsmeisterschaftPlatzierungenVonSchuetzentypViewModel()
        {
            vereinsmeisterschaft = 0;
            typ = VereinsmeisterschaftSchuetzeTyp.maennlich16_30;
            Title = "Übersicht Schützenergebnisse";
            Messenger.Default.Register<LoadVereinsmeisterschaftPlatzierungenVonSchuetzentypMessage>(this, "VereinsmeisterschaftPlatzierungenSchuetzenBereich", m => ReceiveLoadVereinsmeisterschaftPlatzierungenSchuetzenMessage(m));
        }

        private async void ReceiveLoadVereinsmeisterschaftPlatzierungenSchuetzenMessage(LoadVereinsmeisterschaftPlatzierungenVonSchuetzentypMessage m)
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
