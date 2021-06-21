using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Logic.Messages.SchluesselMessages;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungSchluesselUebersichtDetailViewModel : ViewModelUebersicht<SchluesselzuteilungModel>
    {
        private int schluesselid;
        public SchluesselverteilungSchluesselUebersichtDetailViewModel()
        {
            MessageToken = "SchluesselverteilungSchluesselUebersichtDetail";
            Title = "Übersicht der Besitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
            Messenger.Default.Register<LoadSchluesselverteilungSchluesselDetailMessage>(this, "SchluesselverteilungSchluesselUebersicht", m => ReceiveLoadSchluesselverteilungSchluesselDetailMessage(m));
        }

        private void ReceiveLoadSchluesselverteilungSchluesselDetailMessage(LoadSchluesselverteilungSchluesselDetailMessage m)
        {
            schluesselid = m.ID;
            LoadData(schluesselid);
        }

        protected override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            LoadData(schluesselid);
        }

        public async override void LoadData(int id)
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/schluessel/{id}/besitzer");
                if (resp2.IsSuccessStatusCode)
                    itemList = await resp2.Content.ReadAsAsync<ObservableCollection<SchluesselzuteilungModel>>();
            }
            base.LoadData();
        }

        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/{selectedItem.ID}");
                if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
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
