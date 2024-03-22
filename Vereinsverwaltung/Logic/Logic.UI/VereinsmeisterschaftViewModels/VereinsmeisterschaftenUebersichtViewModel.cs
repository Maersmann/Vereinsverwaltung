using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftenUebersichtViewModel : ViewModelUebersicht<VereinsmeisterschaftMitInfoModel, StammdatenTypes>
    {
        public VereinsmeisterschaftenUebersichtViewModel()
        {
            MessageToken = "VereinsmeisterschaftenUebersicht";
            Title = "Übersicht Vereinsmeisterschaften";
            ErgebnisSchuetzenCommand = new RelayCommand(() => ExecuteErgebnisSchuetzenCommand());
            ErgebnisGruppenCommand = new RelayCommand(() => ExecuteErgebnisGruppenCommand());
        }

        protected override string GetREST_API() { return $"/api/Vereinsmeisterschaften/abgeschlossene"; }
        protected override bool WithPagination() { return true; }

        #region Bindings
        public ICommand ErgebnisSchuetzenCommand { get; set; }
        public ICommand ErgebnisGruppenCommand { get; set; }
        #endregion

        #region Commands
        private void ExecuteErgebnisSchuetzenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenVereinsmeisterschaftPlatzierungenSchuetzentypenMessage { VereinsmeisterschaftID = SelectedItem.ID }, messageToken);
        }

        private void ExecuteErgebnisGruppenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenVereinsmeistschaftPlatzierungenGruppentypenMessage { VereinsmeisterschaftID = SelectedItem.ID }, messageToken);
        }
        #endregion
    }
}
