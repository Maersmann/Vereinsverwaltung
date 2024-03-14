using Base.Logic.Core;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.SchnurrschiessenModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.SchnurschiessenMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class AktivesSchnurschiessenMitgliederUebersichtViewModel : ViewModelUebersicht<AktivesSchnurschiessenMitgliederUebersichtModel, StammdatenTypes>
    {
        private AktivesSchnurschiessenModel aktiveSchnurschiessen;

        public AktivesSchnurschiessenMitgliederUebersichtViewModel()
        {
            MessageToken = "AktivesSchnurschiessenMitgliederUebersicht";
            Title = "Aktives Schnurschießen - Mitglieder Übersicht";
            _ = LoadSchnurschiessen();

            RegisterAktualisereViewMessage(StammdatenTypes.schnurschiessen.ToString());
            TeilnahmeEintragenCommand = new DelegateCommand(ExecuteTeilnahmeEintragenCommand, CanExecuteCommand);
        }

        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.mitglied; }
        protected override int GetID() { return SelectedItem.MitgliedID; }
        protected override string GetREST_API() { return $"/api/Schnurschiessen/Aktiv/Mitglieder?schnurschiessenID={aktiveSchnurschiessen.ID}"; }
        protected override bool WithPagination() { return true; }

        protected override bool LoadingOnCreate() => false;


        private async Task LoadSchnurschiessen()
        {
            aktiveSchnurschiessen = new AktivesSchnurschiessenModel();
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                string URL = GlobalVariables.BackendServer_URL + "/api/Schnurschiessen/Aktiv";
                HttpResponseMessage resp = await Client.GetAsync(URL);
                if (resp.IsSuccessStatusCode)
                {
                    Response<AktivesSchnurschiessenModel> VereinsmeisterschaftResponse = await resp.Content.ReadAsAsync<Response<AktivesSchnurschiessenModel>>();
                    aktiveSchnurschiessen = VereinsmeisterschaftResponse.Data;
                    await LoadData();
                    RequestIsWorking = false;
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    RequestIsWorking = false;
                    SendExceptionMessage("Kein Schnurschiessen aktiv.");
                }
            }
        }


        #region Bindings

        public ICommand TeilnahmeEintragenCommand { get; set; }

        #endregion
        #region Commands

        private void ExecuteTeilnahmeEintragenCommand()
        {
            Messenger.Default.Send(new OpenAktivesSchnurschiessenTeilnahmeEintragenMessage { MitgliedId = SelectedItem.MitgliedID }, messageToken);
        }

        protected async override void ExecuteBearbeitenCommand()
        {
            base.ExecuteBearbeitenCommand();
            FilterText = SelectedItem.Name;
            await LoadData();
        }

        #endregion

    }
}
