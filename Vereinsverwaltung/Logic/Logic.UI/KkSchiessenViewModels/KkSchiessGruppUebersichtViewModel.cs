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
    public class KkSchiessGruppUebersichtViewModel : ViewModelUebersicht<KkSchiessgruppeModel, StammdatenTypes>
    {
        public KkSchiessGruppUebersichtViewModel()
        {
            Title = "Übersicht KK-Schießgruppen";
            RegisterAktualisereViewMessage(StammdatenTypes.kkSchiessgruppe.ToString());
        }

        protected override int GetID() { return selectedItem.ID; }
        protected override string GetREST_API() { return $"/api/KKSchiessGruppen"; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.kkSchiessgruppe; }

        #region Commands

        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/KKSchiessGruppen/{selectedItem.ID}");
                if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("KK-Schießgruppe kann nicht gelöscht werden");
                    return;
                }
                else if((int)resp.StatusCode == 910)
                {
                    SendExceptionMessage("KK-Schießgruppe kann nicht gelöscht werden" + Environment.NewLine+ Environment.NewLine + "Gruppe hat schon an KK-Schießen teilgenommen");
                    return;
                }

                SendInformationMessage("KK-Schießgruppe gelöscht");
                base.ExecuteEntfernenCommand();
                RequestIsWorking = false;
            }
        }
        #endregion
    }
}
