using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Logic.Messages.SchluesselMessages;
using Logic.UI.BaseViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungBesitzerUebersichtDetailViewModel : ViewModelUebersicht<SchluesselzuteilungModel>
    {
        private int besitzerid;
        public SchluesselverteilungBesitzerUebersichtDetailViewModel()
        {
            MessageToken = "SchluesselzuteilungBesitzerSchluesselUebersicht";
            Title = "Vorhandene Schlüssel des Besitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
            Messenger.Default.Register<LoadSchluesselverteilungBesitzerDetailMessage>(this, "SchluesselverteilungBesitzerUebersicht", m => ReceiveLoadSchluesselverteilungBesitzerDetailMessage(m));
        }

        private void ReceiveLoadSchluesselverteilungBesitzerDetailMessage(LoadSchluesselverteilungBesitzerDetailMessage m)
        {
            besitzerid = m.ID;
            LoadData(besitzerid);
        }

        protected override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            LoadData(besitzerid);
        }

        public override async void LoadData(int id)
        {
            if (GlobalVariables.ServerIsOnline)
            {
                DataIsLoading = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/besitzer/{id}/schluessel");
                if (resp.IsSuccessStatusCode)
                {
                    itemList = await resp.Content.ReadAsAsync<ObservableCollection<SchluesselzuteilungModel>>();
                }

                DataIsLoading = false;
            }
            base.LoadData(id);
        }

        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/{selectedItem.ID}");
                if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Eintrag konnte nicht gelöscht werden.");
                    return;
                }
            }
            SendInformationMessage("Eintrag gelöscht");
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.schluesselzuteilung);
            base.ExecuteEntfernenCommand();
        }
        #endregion
    }
}
