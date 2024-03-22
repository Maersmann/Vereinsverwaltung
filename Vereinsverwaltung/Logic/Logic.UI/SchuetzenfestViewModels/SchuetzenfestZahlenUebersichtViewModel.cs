using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.KkSchiessenModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Data.Model.SchuetzenfestModels;

namespace Logic.UI.SchuetzenfestViewModels
{
    public class SchuetzenfestZahlenUebersichtViewModel : ViewModelUebersicht<SchuetzenfestZahlenModel, StammdatenTypes>
    {
        public SchuetzenfestZahlenUebersichtViewModel()
        {
            Title = "Übersicht Zahlen Schützenfest";
            RegisterAktualisereViewMessage(StammdatenTypes.schuetzenfestZahlen.ToString());
        }

        protected override int GetID() { return SelectedItem.ID; }
        protected override string GetREST_API() { return $"/api/SchuetzenfestZahlen"; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schuetzenfestZahlen; }
        protected override bool WithPagination() { return true; }

        #region Commands

        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/SchuetzenfestZahlen/{SelectedItem.ID}");
                if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("Zahlen können nicht gelöscht werden");
                    return;
                }

                SendInformationMessage("Zahlen gelöscht");
                base.ExecuteEntfernenCommand();
                RequestIsWorking = false;
            }
        }
        #endregion
    }
}
