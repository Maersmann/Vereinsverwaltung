using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
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
            Messenger.Default.Register<LoadSchluesselverteilungSchluesselDetailMessage>(this, "SchluesselverteilungSchluesselUebersicht", m => ReceiveLoadSchluesselverteilungSchluesselDetailMessage(m));
        }

        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/zuteilung/schluessel/{LoadDataID}/besitzer"; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schluesselzuteilung; }

        private void ReceiveLoadSchluesselverteilungSchluesselDetailMessage(LoadSchluesselverteilungSchluesselDetailMessage m)
        {
            schluesselid = m.ID;
            LoadData(schluesselid);
        }

        protected override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            LoadData(schluesselid);
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
