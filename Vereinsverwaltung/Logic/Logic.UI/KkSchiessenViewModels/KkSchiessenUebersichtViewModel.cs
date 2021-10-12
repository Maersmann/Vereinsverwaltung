using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.KkSchiessenModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Logic.UI.KkSchiessenViewModels
{
    public class KkSchiessenUebersichtViewModel : ViewModelUebersicht<KkSchiessenModel, StammdatenTypes>
    {
        public KkSchiessenUebersichtViewModel()
        {
            Title = "Übersicht KK-Schießen";
            RegisterAktualisereViewMessage(StammdatenTypes.kkSchiessen.ToString());
        }

        protected override int GetID() { return selectedItem.ID; }
        protected override string GetREST_API() { return $"/api/KkSchiessen"; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.kkSchiessen; }

        #region Commands

        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/KkSchiessen/{selectedItem.ID}");
                if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("KK-Schießen kann nicht gelöscht werden");
                    return;
                }

                SendInformationMessage("KK-Schießen gelöscht");
                base.ExecuteEntfernenCommand();
                RequestIsWorking = false;
            }
        }
        #endregion
    }
}
