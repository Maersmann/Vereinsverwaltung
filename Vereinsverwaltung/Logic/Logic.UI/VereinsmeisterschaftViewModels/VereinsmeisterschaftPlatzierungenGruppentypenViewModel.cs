using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftPlatzierungenGruppentypenViewModel : ViewModelUebersicht<VereinsmeisterschaftGruppenErgebnisseJeBereichModel, StammdatenTypes>
    {
        private int? vereinsmeisterschaftID;
        public VereinsmeisterschaftPlatzierungenGruppentypenViewModel()
        {
            vereinsmeisterschaftID = null;
            MessageToken = "VereinsmeisterschaftPlatzierungenGruppenBereich";
            Title = "Übersicht Bereiche Vereinsmeisterschaft";
        }

        protected override string GetREST_API() { return $"/api/VereinmeisterschaftGruppeErgebnisse/Vereinsmeisterschaft/Bereiche?vereinsmeisterschaftId={vereinsmeisterschaftID ?? 0}"; }
        protected override bool WithPagination() { return true; }
        protected override bool LoadingOnCreate() => false;

        public int VereinsmeisterschaftID
        {
            set
            {
                vereinsmeisterschaftID = value;
                _ = LoadData();
            }
        }

        #region Bindings
        public override VereinsmeisterschaftGruppenErgebnisseJeBereichModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                if (SelectedItem != null)
                {
                    WeakReferenceMessenger.Default.Send(new LoadVereinsmeisterschaftPlatzierungenVonGruppentypMessage { GruppeTyp = SelectedItem.VereinsmeisterschaftGruppeTyp, VereinsmeisterschaftID = vereinsmeisterschaftID.Value }, messageToken);
                }
            }
        }
        #endregion
    }
}
