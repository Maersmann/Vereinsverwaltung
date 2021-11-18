using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.UserModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.UserMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.UserViewModels
{
    public class UserUebersichtViewModel : ViewModelUebersicht<UserModel, StammdatenTypes>
    {

        public UserUebersichtViewModel()
        {
            MessageToken = "UserUebersicht";
            Title = "Übersicht User";
            RegisterAktualisereViewMessage(StammdatenTypes.user.ToString());
            BereichtigungCommand = new DelegateCommand(ExecuteBereichtigungCommand, CanExecuteCommand);
        }

        protected override int GetID() { return SelectedItem.Id; }
        protected override string GetREST_API() { return $"/api/Users"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.user; }

        #region Bindings
        public ICommand BereichtigungCommand { get; set; }
        public override UserModel SelectedItem 
        { 
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                ((DelegateCommand)BereichtigungCommand).RaiseCanExecuteChanged();
            } 
        }
        #endregion

        #region Commands

        private void ExecuteBereichtigungCommand()
        {
            Messenger.Default.Send(new OpenUserBerechtigungenMessage { UserID = SelectedItem.Id }, messageToken);
        }

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
