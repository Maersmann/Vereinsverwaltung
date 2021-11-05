using Data.Model.MitgliederModels;
using System;
using Data.Types;
using Logic.Core;
using System.Net.Http;
using System.Net;
using Base.Logic.ViewModels;
using Base.Logic.Core;
using System.Windows.Input;
using System.Windows.Controls;
using GalaSoft.MvvmLight.CommandWpf;

namespace Logic.UI.MitgliederViewModels
{
    public class MitgliederUebersichtViewModel : ViewModelUebersicht<MitgliederModel, StammdatenTypes>
    {

        public MitgliederUebersichtViewModel()
        {
            Title = "Übersicht Mitglieder";
            RegisterAktualisereViewMessage(StammdatenTypes.mitglied.ToString());
        }
        
        protected override int GetID() { return SelectedItem.ID; }
        protected override string GetREST_API() { return $"/api/Mitglieder"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.mitglied; }

        #region Commands

        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder/{SelectedItem.ID}");
                if ((int)resp.StatusCode == 903)
                {
                    SendExceptionMessage("Mitglied kann nicht gelöscht werden" + Environment.NewLine + Environment.NewLine + "Zugeteilter Schlüssel vorhanden");
                    return;
                }
            
                SendInformationMessage("Mitglied gelöscht");
                base.ExecuteEntfernenCommand();
                RequestIsWorking = false;
            }
        }
        #endregion

    }
}
