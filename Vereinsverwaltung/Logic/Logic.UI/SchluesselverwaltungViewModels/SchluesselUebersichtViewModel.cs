using Base.Logic.Core;
using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.SchluesselMessages;
using Logic.UI.BaseViewModels;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselModel, StammdatenTypes>
    {
        public SchluesselUebersichtViewModel()
        {
            MessageToken = "SchluesselUebersicht";
            Title = "Übersicht Schlüssel";
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel.ToString());
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung.ToString());
            OpenHistorieCommand = new DelegateCommand(ExecuteOpenHistorieCommand, CanExecuteCommand);
        }

        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schluessel; }
        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Schluessel; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/schluessel"; }

        #region Bindings
        public ICommand OpenHistorieCommand { get; set; }

        public override SchluesselModel SelectedItem 
        { 
            get => base.SelectedItem;
            set
            {
                ((DelegateCommand)OpenHistorieCommand).RaiseCanExecuteChanged();
                base.SelectedItem = value;
            }
        }
        #endregion

        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/schluessel/{selectedItem.ID}");

                if ((int)resp.StatusCode == 905)
                {
                    SendExceptionMessage("Schlüssel kann nicht gelöscht werden" + Environment.NewLine + Environment.NewLine + "Schlüssel ist Besitzer zugeordnet");
                    RequestIsWorking = false;
                    return;
                }
                SendInformationMessage("Schlüssel gelöscht");
                base.ExecuteEntfernenCommand();
                RequestIsWorking = false;
            }
        }

        private void ExecuteOpenHistorieCommand()
        {
            Messenger.Default.Send<OpenSchluesselzuteilungHistoryUebersichtMessage>(new OpenSchluesselzuteilungHistoryUebersichtMessage { AuswahlTypes = SchluesselzuteilungTypes.Schluessel, ID = selectedItem.ID}, messageToken);
        }

        #endregion
    }
}
