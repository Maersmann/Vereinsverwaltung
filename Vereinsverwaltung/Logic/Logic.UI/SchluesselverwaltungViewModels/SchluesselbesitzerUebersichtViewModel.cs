using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.SchluesselMessages;
using Logic.UI.BaseViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselbesitzerUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselbesitzerModel>
    {
        public SchluesselbesitzerUebersichtViewModel()
        {
            MessageToken = "SchluesselbesitzerUebersicht";
            Title = "Übersicht Schlüsselbesitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselbesitzer);
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
            OpenHistorieCommand = new DelegateCommand(this.ExecuteOpenHistorieCommand, this.CanExecuteCommand);
        }

        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluesselbesitzer; }
        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Besitzer; }
        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer");
                if (resp2.IsSuccessStatusCode)
                    itemList = await resp2.Content.ReadAsAsync<ObservableCollection<SchluesselbesitzerModel>>();
            }
            base.LoadData();
        }

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
                HttpResponseMessage resp2 = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer/{selectedItem.ID}");
                if (resp2.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("Besitzer kann nicht gelöscht werden" + Environment.NewLine +  Environment.NewLine + "Besitzer sind Schlüssel zugeordnet");
                    return;
                }
                SendInformationMessage("Schlüsselbesitzer gelöscht");
                base.ExecuteEntfernenCommand();
            }
        }

        private void ExecuteOpenHistorieCommand()
        {
            Messenger.Default.Send<OpenSchluesselzuteilungHistoryUebersichtMessage>(new OpenSchluesselzuteilungHistoryUebersichtMessage { AuswahlTypes = SchluesselzuteilungTypes.Besitzer, ID = selectedItem.ID }, messageToken);
        }

        #endregion
    }
}
