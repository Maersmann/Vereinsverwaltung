using Base.Logic.Core;
using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using CommunityToolkit.Mvvm.Messaging;
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
    public class SchluesselbesitzerUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselbesitzerModel, StammdatenTypes>
    {
        public SchluesselbesitzerUebersichtViewModel()
        {
            MessageToken = "SchluesselbesitzerUebersicht";
            Title = "Übersicht Schlüsselbesitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselbesitzer.ToString());
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung.ToString());
            OpenHistorieCommand = new DelegateCommand(this.ExecuteOpenHistorieCommand, CanExecuteCommand);
        }

        protected override int GetID() { return SelectedItem.ID; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schluesselbesitzer; }
        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Besitzer; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/besitzer"; }
        protected override bool WithPagination() { return true; }

        #region Bindings
        public ICommand OpenHistorieCommand { get; set; }

        public override SchluesselbesitzerModel SelectedItem
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
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer/{SelectedItem.ID}");
                if ((int)resp.StatusCode == 903)
                {
                    SendExceptionMessage("Besitzer kann nicht gelöscht werden" + Environment.NewLine +  Environment.NewLine + "Besitzer sind Schlüssel zugeordnet");
                    RequestIsWorking = false;
                    return;
                }
                SendInformationMessage("Schlüsselbesitzer gelöscht");
                base.ExecuteEntfernenCommand();
                RequestIsWorking = false;
            }
        }

        private void ExecuteOpenHistorieCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenSchluesselzuteilungHistoryUebersichtMessage { AuswahlTypes = SchluesselzuteilungTypes.Besitzer, ID = SelectedItem.ID }, messageToken);
        }

        #endregion
    }
}
