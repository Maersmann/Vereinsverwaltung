using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;
using Prism.Commands;
using System;
using System.Net.Http;
using System.Windows.Input;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class SchuetzenUebersichtViewModel : ViewModelUebersicht<SchuetzeModel, StammdatenTypes>
    {

        public SchuetzenUebersichtViewModel()
        {
            MessageToken = "SchuetzenUebersicht";
            Title = "Übersicht Schützen";
            RegisterAktualisereViewMessage(StammdatenTypes.schuetze.ToString());
            OpenGruppenCommand = new DelegateCommand(ExecuteOpenGruppenCommand, CanExecuteCommand);
            OpenErgebnisseCommand = new DelegateCommand(ExecuteErgebnisseCommand, CanExecuteCommand);
        }

        protected override int GetID() { return SelectedItem.ID; }
        protected override string GetREST_API() { return $"/api/Schuetzen"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schuetze; }

        #region Bindings
        public ICommand OpenGruppenCommand { get; set; }
        public ICommand OpenErgebnisseCommand { get; set; }
        public override SchuetzeModel SelectedItem 
        { 
            get => base.SelectedItem; 
            set
            {
                base.SelectedItem = value;
                ((DelegateCommand)OpenGruppenCommand).RaiseCanExecuteChanged();
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
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/Schuetzen/{SelectedItem.ID}");
                RequestIsWorking = false;
                if ((int)resp.StatusCode == 913)
                {
                    SendExceptionMessage("Der Schütze kann nicht gelöscht werden." + Environment.NewLine + "Es wurde an eine Vereinsmeisterschaft teilgenommen.");
                    return;
                }
                SendInformationMessage("Schütze gelöscht");
                base.ExecuteEntfernenCommand();
            }
        }

        private void ExecuteOpenGruppenCommand()
        {
            Messenger.Default.Send(new OpenVereinsmeisterschaftGruppenVonSchuetzeMessage { SchuetzeID = SelectedItem.ID }, messageToken);
        }

        private void ExecuteErgebnisseCommand()
        {
            Messenger.Default.Send(new OpenVereinsmeisterschaftSchuetzeErgebnisseMessage { SchuetzeID = SelectedItem.ID }, messageToken);
        }
        #endregion

    }
}
