using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.UserModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Logic.UI.UserViewModels
{
    public class UserUebersichtViewModel : ViewModelUebersicht<UserModel, StammdatenTypes>
    {

        public UserUebersichtViewModel()
        {
            Title = "Übersicht User";
            RegisterAktualisereViewMessage(StammdatenTypes.user.ToString());
        }

        protected override int GetID() { return SelectedItem.Id; }
        protected override string GetREST_API() { return $"/api/Users"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.user; }

        #region Commands

        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                _ = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/Users/{SelectedItem.Id}");

                SendInformationMessage("User gelöscht");
                base.ExecuteEntfernenCommand();
                RequestIsWorking = false;
            }
        }
        #endregion

    }
}
