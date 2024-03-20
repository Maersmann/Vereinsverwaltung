using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftPlatzierungenSchuetzentypenViewModel : ViewModelUebersicht<VereinsmeisterschaftSchuetzeErgebnisseJeBereichModel, StammdatenTypes>
    {
        private int? vereinsmeisterschaftID;
        public VereinsmeisterschaftPlatzierungenSchuetzentypenViewModel()
        {
            vereinsmeisterschaftID = null;
            MessageToken = "VereinsmeisterschaftPlatzierungenSchuetzenBereich";
            Title = "Übersicht Bereiche Vereinsmeisterschaft";
        }

        protected override string GetREST_API() { return $"/api/VereinsmeisterschaftschuetzeErgebnisse/Vereinsmeisterschaft/Bereiche?vereinsmeisterschaftId={vereinsmeisterschaftID ?? 0}"; }
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
        public override VereinsmeisterschaftSchuetzeErgebnisseJeBereichModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                if (SelectedItem != null)
                {
                    WeakReferenceMessenger.Default.Send(new LoadVereinsmeisterschaftPlatzierungenVonSchuetzentypMessage { SchuetzeTyp = SelectedItem.VereinsmeisterschaftSchuetzeTyp, VereinsmeisterschaftID = vereinsmeisterschaftID.Value }, messageToken);
                }
            }
        }
        #endregion
    }
}
