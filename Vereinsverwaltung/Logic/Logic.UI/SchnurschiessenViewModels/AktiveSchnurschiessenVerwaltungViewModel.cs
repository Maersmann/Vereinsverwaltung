using Base.Logic.Core;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.SchnurrschiessenModels;
using Data.Types;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.SchnurschiessenMessages;
using Logic.Messages.VereinsmeisterschaftMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Logic.Messages;
using Logic.Messages.BaseMessages;
using Data.Model.SchnurrschiessenModels.DTO;
using Logic.Messages.UtilMessages;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class AktiveSchnurschiessenVerwaltungViewModel : ViewModelUebersicht<AktivesSchnurschiessenSchnurBestandModel, StammdatenTypes>
    {
        private AktivesSchnurschiessenModel aktiveSchnurschiessen;
        private bool schnurschiessenBeendet;
        public AktiveSchnurschiessenVerwaltungViewModel()
        {
            MessageToken = "AktiveSchnurschiessenVerwaltung";
            Title = "Verwaltung Schnurrschiessen";
            schnurschiessenBeendet = false;
            _ = LoadSchnurschiessen();
            RegisterAktualisereViewMessage(StammdatenTypes.schnurschiessenAuszeichnungBestand.ToString());
            SchnurAusgabeCommand = new DelegateCommand(ExecuteSchnurAusgabeCommand, CanExecuteCommand);
            SchnurschiessenBeendenCommand = new DelegateCommand(ExecuteSchnurschiessenBeendenCommand, CanExecuteCommand);
        }

        protected override int GetID() { return SelectedItem.SchnurschiessenBestandID; }
        protected override string GetREST_API() { return $"/api/Schnurschiessen/AuszeichnungBestand?schnurschiessenID={aktiveSchnurschiessen.ID}"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schnurschiessenAuszeichnungBestand; }
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
                else if (resp.StatusCode.Equals(HttpStatusCode.NotFound) && !schnurschiessenBeendet)
                {
                    RequestIsWorking = false;
                    if (BerechtigungenService.HatBerechtigung(BerechtigungTypes.SchnurschiessenVerwaltung))
                    {
                        WeakReferenceMessenger.Default.Send(new NeuesSchnurschiessenErstellenMessage(NeuesSchnurschiessenErstellenCallback), messageToken);
                    }
                    else
                    {
                        SendExceptionMessage("Kein Schnurschiessen aktiv.");
                    }
                    
                }
            }
            ((DelegateCommand)SchnurAusgabeCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)SchnurschiessenBeendenCommand).RaiseCanExecuteChanged();
        }

        #region Bindings

        public ICommand SchnurAusgabeCommand { get; set; }
        public ICommand SchnurschiessenBeendenCommand { get; set; }
        public override AktivesSchnurschiessenSchnurBestandModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                if (SelectedItem != null)
                {
                    WeakReferenceMessenger.Default.Send(new LoadAktiveSchnurschiessenBestandHistorieMessage { SchnurschiessenBestandID = SelectedItem.SchnurschiessenBestandID }, messageToken);
                }
            }
        }
        #endregion
        #region Commands

        protected override bool CanExecuteCommand() => aktiveSchnurschiessen.ID > 0;

        private void ExecuteSchnurschiessenBeendenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenBestaetigungViewMessage { Beschreibung = "Soll das Schnurschießen beendet werden?", Command = SchnurschiessenBeenden }, "AktiveSchnurschiessenVerwaltung");


        }
        private async void SchnurschiessenBeenden()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/schnurschiessen/abschliessen", new SchnurschiessenAbschliessenDTO { ID = aktiveSchnurschiessen.ID });
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    schnurschiessenBeendet = true;
                    await LoadSchnurschiessen();
                    WeakReferenceMessenger.Default.Send(new LoadAktiveSchnurschiessenBestandHistorieMessage { SchnurschiessenBestandID = 0 }, messageToken);

                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Schnurschiessen beendet" }, GetStammdatenTyp().ToString());
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Schnurschiessen konnte nicht beendet werden.");
                }
            }
        }

        private void ExecuteSchnurAusgabeCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage { SchnurschiessenBestandID = SelectedItem.SchnurschiessenBestandID, Bezeichnung = SelectedItem.Bezeichnung }, messageToken);
        }


        #endregion

        #region Callback
        private async void NeuesSchnurschiessenErstellenCallback(bool confirmed)
        {
            if (confirmed)
            {
                await LoadSchnurschiessen();
            }
        }
        #endregion
    }
}
