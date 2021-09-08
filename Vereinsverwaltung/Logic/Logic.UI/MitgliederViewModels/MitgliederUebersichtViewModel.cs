using Data.Model.MitgliederModels;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Logic.UI.BaseViewModels;
using Data.Types;
using Logic.Core;
using System.Net.Http;
using System.Net;

namespace Logic.UI.MitgliederViewModels
{
    public class MitgliederUebersichtViewModel : ViewModelUebersicht<MitgliederModel>
    {

        public MitgliederUebersichtViewModel()
        {
            Title = "Übersicht Mitglieder";
            //RegisterAktualisereViewMessage(StammdatenTypes.mitglied);    
        }
        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.mitglied; }

        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder");
                if (resp.IsSuccessStatusCode)
                    itemList = await resp.Content.ReadAsAsync<ObservableCollection<MitgliederModel>>();
            }
            base.LoadData();
        }

        #region Commands

        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.DeleteAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder/{selectedItem.ID}");
                if (resp2.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("Mitglied kann nicht gelöscht werden" + Environment.NewLine + Environment.NewLine + "Zugeteilter Schlüssel vorhanden");
                    return;
                }
            }
            SendInformationMessage("Mitglied gelöscht");
            base.ExecuteEntfernenCommand();
         
        }
        #endregion
    }
}
