using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Logic.Messages.SchluesselMessages;
using Base.Logic.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using Base.Logic.Messages;
using Base.Logic.Core;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungBesitzerUebersichtDetailViewModel : ViewModelUebersicht<SchluesselzuteilungModel, StammdatenTypes>
    {
        private int besitzerid;
        public SchluesselverteilungBesitzerUebersichtDetailViewModel()
        {
            MessageToken = "SchluesselzuteilungBesitzerSchluesselUebersicht";
            Title = "Vorhandene Schlüssel des Besitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung.ToString());
            Messenger.Default.Register<LoadSchluesselverteilungBesitzerDetailMessage>(this, "SchluesselverteilungBesitzerUebersicht", m => ReceiveLoadSchluesselverteilungBesitzerDetailMessage(m));
        }

        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/zuteilung/besitzer/{LoadDataID}/schluessel"; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schluesselzuteilung; }

        private void ReceiveLoadSchluesselverteilungBesitzerDetailMessage(LoadSchluesselverteilungBesitzerDetailMessage m)
        {
            besitzerid = m.ID;
            LoadData(besitzerid);
        }

        protected override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            LoadData(besitzerid);
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
            Messenger.Default.Send(new AktualisiereViewMessage(), StammdatenTypes.schluesselzuteilung.ToString());
            base.ExecuteEntfernenCommand();
            RequestIsWorking = false;
        }
        #endregion
    }
}
