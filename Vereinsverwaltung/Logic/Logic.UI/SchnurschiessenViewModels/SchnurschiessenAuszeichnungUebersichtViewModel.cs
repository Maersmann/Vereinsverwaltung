using Data.Model.SchnurrschiessenModels;
using Data.Types;
using System.Net;
using Base.Logic.ViewModels;
using Base.Logic.Core;
using System.Net.Http;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenAuszeichnungUebersichtViewModel : ViewModelUebersicht<SchnurschiessenAuszeichnungModel, StammdatenTypes>
    {
        public SchnurschiessenAuszeichnungUebersichtViewModel()
        {
            MessageToken = "SchnurschiessenAuszeichnungUebersicht";
            Title = "Übersicht Auszeichnungen";
            RegisterAktualisereViewMessage(StammdatenTypes.schnurschiessenAuszeichnung.ToString());
        }

        protected override int GetID() { return SelectedItem.ID; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schnurschiessenAuszeichnung; }
        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/Schnurschiessenauszeichnung"; }

        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/Schnurschiessenauszeichnung/{SelectedItem.ID}");
                if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                    RequestIsWorking = false;
                    return;
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("Auszeichnung konnte nicht gelöscht werden.");
                    RequestIsWorking = false;
                    return;
                }
            }
            SendInformationMessage("Auszeichnung gelöscht");
            base.ExecuteEntfernenCommand();
            RequestIsWorking = false;
        }

        #endregion
    }
}
