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
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/besitzer/{id}/schluessel");
                if (resp.IsSuccessStatusCode)
                {
                    itemList = await resp.Content.ReadAsAsync<ObservableCollection<SchluesselzuteilungModel>>();
                }

                RequestIsWorking = false;
            }
            base.LoadData(id);
        }

        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/{selectedItem.ID}");
                if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Eintrag konnte nicht gelöscht werden.");
                    RequestIsWorking = false;
                    return;
                }
            }
            SendInformationMessage("Eintrag gelöscht");
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.schluesselzuteilung.ToString());
            base.ExecuteEntfernenCommand();
            RequestIsWorking = false;
        }
        #endregion
    }
}
