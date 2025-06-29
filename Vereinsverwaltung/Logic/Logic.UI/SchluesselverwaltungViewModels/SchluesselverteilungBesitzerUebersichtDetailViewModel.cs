using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Logic.Messages.SchluesselMessages;
using Base.Logic.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using Base.Logic.Messages;
using Base.Logic.Core;
using System.Windows.Input;
using Prism.Commands;
using Data.Types.SchluesselverwaltungTypes;
using System.Threading.Tasks;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungBesitzerUebersichtDetailViewModel : ViewModelUebersicht<SchluesselzuteilungModel, StammdatenTypes>
    {
        private int besitzerid;
        public SchluesselverteilungBesitzerUebersichtDetailViewModel()
        {
            MessageToken = "SchluesselverteilungBesitzerUebersichtDetail";
            Title = "Vorhandene Schlüssel des Besitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung.ToString());
            WeakReferenceMessenger.Default.Register<LoadSchluesselverteilungBesitzerDetailMessage, string>(this, "SchluesselverteilungBesitzerUebersicht", (r,m) => ReceiveLoadSchluesselverteilungBesitzerDetailMessage(m));
            OpenKennungEintragenCommand = new DelegateCommand(ExecuteOpenKennungEintragenCommand, CanExecuteOpenKennungEintragenCommand);
        }
        private bool CanExecuteOpenKennungEintragenCommand()
        {
            return ItemList.Count > 0;
        }

        public ICommand OpenKennungEintragenCommand { get; set; }

        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/zuteilung/besitzer/{LoadDataID}/schluessel"; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schluesselzuteilung; }

        public override async Task LoadData(int id)
        {
            await base.LoadData(id);
            ((DelegateCommand)OpenKennungEintragenCommand).RaiseCanExecuteChanged();
        }



        private async void ReceiveLoadSchluesselverteilungBesitzerDetailMessage(LoadSchluesselverteilungBesitzerDetailMessage m)
        {
            besitzerid = m.ID;
            await LoadData(besitzerid);
        }

        protected async override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            await LoadData(besitzerid);
        }

        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/{SelectedItem.ID}");
                if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Eintrag konnte nicht gelöscht werden.");
                    RequestIsWorking = false;
                    return;
                }
            }
            SendInformationMessage("Eintrag gelöscht");
            WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), StammdatenTypes.schluesselzuteilung.ToString());
            base.ExecuteEntfernenCommand();
            RequestIsWorking = false;
        }

        private void ExecuteOpenKennungEintragenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenSchluesselKennungEintragenMessage { Command = async () => await LoadData(besitzerid), ID = SelectedItem.ID }, messageToken);
        }
        #endregion
    }
}
