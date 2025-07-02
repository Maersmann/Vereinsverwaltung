using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Logic.Messages.SchluesselMessages;
using Base.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Base.Logic.Messages;
using Base.Logic.Core;
using Prism.Commands;
using System.Windows.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungSchluesselUebersichtDetailViewModel : ViewModelUebersicht<SchluesselzuteilungModel, StammdatenTypes>
    {
        private int schluesselid;
        public SchluesselverteilungSchluesselUebersichtDetailViewModel()
        {
            MessageToken = "SchluesselverteilungSchluesselUebersichtDetail";
            Title = "Übersicht der Besitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung.ToString());
            WeakReferenceMessenger.Default.Register<LoadSchluesselverteilungSchluesselDetailMessage, string>(this, "SchluesselverteilungSchluesselUebersicht", (r,m) => ReceiveLoadSchluesselverteilungSchluesselDetailMessage(m));
            OpenKennungEintragenCommand = new DelegateCommand(ExecuteOpenKennungEintragenCommand, CanExecuteOpenKennungEintragenCommand);
        }

        public ICommand OpenKennungEintragenCommand { get; set; }

        private bool CanExecuteOpenKennungEintragenCommand()
        {
            return ItemList.Count > 0;
        }
        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/zuteilung/schluessel/{LoadDataID}/besitzer"; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schluesselzuteilung; }

        public override async Task LoadData(int id)
        {
            await base.LoadData(id);
            ((DelegateCommand)OpenKennungEintragenCommand).RaiseCanExecuteChanged();
        }

        private async void ReceiveLoadSchluesselverteilungSchluesselDetailMessage(LoadSchluesselverteilungSchluesselDetailMessage m)
        {
            schluesselid = m.ID;
            await LoadData(schluesselid);
        }

        protected async override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            await LoadData(schluesselid);
        }

        private void ExecuteOpenKennungEintragenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenSchluesselKennungEintragenMessage { Command = async () => await LoadData(schluesselid), ID = SelectedItem.ID }, messageToken);
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
        #endregion

    }
}
