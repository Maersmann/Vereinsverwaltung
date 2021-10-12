using Data.Model.SchnurrschiessenModels;
using Data.Types;
using System.Net;
using Base.Logic.ViewModels;
using Base.Logic.Core;
using System.Net.Http;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurUebersichtViewModel : ViewModelUebersicht<SchnurModel, StammdatenTypes>
    {
        public SchnurUebersichtViewModel()
        {
            MessageToken = "SchnurUebersicht";
            Title = "Übersicht Schnüre";
            RegisterAktualisereViewMessage(StammdatenTypes.schnur.ToString());
        }

        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schnur; }
        protected override string GetREST_API() { return $"/api/schnurschiessen/Schnur/sichtbar"; }

        #region Commands
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnur/{selectedItem.ID}");
                if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("Schnur konnte nicht gelöscht werden.");
                    RequestIsWorking = false;
                    return;
                }
            }
            SendInformationMessage("Schnur gelöscht");
            base.ExecuteEntfernenCommand();
            RequestIsWorking = false;
        }

        #endregion
    }
}
