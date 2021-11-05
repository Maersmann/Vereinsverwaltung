using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class SchiessgruppenUebersichtViewModel : ViewModelUebersicht<SchiessgruppeModel, StammdatenTypes>
    {

        public SchiessgruppenUebersichtViewModel()
        {
            MessageToken = "SchiessgruppenUebersicht";
            Title = "Übersicht Schiessgruppen";
            RegisterAktualisereViewMessage(StammdatenTypes.schiessgruppe.ToString());
            OpenSchuetzenCommand = new DelegateCommand(ExecuteOpenSchuetzenCommand, CanExecuteCommand);
            OpenErgebnisseCommand = new DelegateCommand(ExecuteErgebnisseCommand, CanExecuteCommand);
        }

        protected override int GetID() { return SelectedItem.ID; }
        protected override string GetREST_API() { return $"/api/Schiessgruppen"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schiessgruppe; }
        #region Bindings
        public ICommand OpenSchuetzenCommand { get; set; }
        public ICommand OpenErgebnisseCommand { get; set; }
        public override SchiessgruppeModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                ((DelegateCommand)OpenSchuetzenCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)OpenErgebnisseCommand).RaiseCanExecuteChanged();
            }
        }
        #endregion
        #region Commands

        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/VereinsmeisterschaftenGruppen/{SelectedItem.ID}");
                RequestIsWorking = false;
                if ((int)resp.StatusCode == 914)
                {
                    SendExceptionMessage("Die Gruppe kann nicht gelöscht werden." + Environment.NewLine + "Sie hat an eine Vereinsmeisterschaft teilgenommen.");
                    return;
                }
                SendInformationMessage("Gruppe gelöscht");
                base.ExecuteEntfernenCommand(); 
            }
        }

        private void ExecuteOpenSchuetzenCommand()
        {
            Messenger.Default.Send(new OpenVereinsmeisterschaftSchuetzenDerGruppeMessage { SchiessgruppeID = SelectedItem.ID }, messageToken);
        }

        private void ExecuteErgebnisseCommand()
        {
            Messenger.Default.Send(new OpenVereinsmeisterschaftGruppeErgebnisseMessage { SchiessgruppeID = SelectedItem.ID }, messageToken);
        }
        #endregion
    }
}
