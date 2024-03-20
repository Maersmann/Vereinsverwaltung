using Base.Logic.Core;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.UserModels;
using Data.Types;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Controls;

namespace Logic.UI.UserViewModels
{
    public class UserPasswordAendernViewModel : ViewModelStammdaten<UserModel, StammdatenTypes>
    {
        public UserPasswordAendernViewModel()
        {
            Title = "Password ändern";
            PasswordCommand = new RelayCommand<PasswordBox>(ExecutePasswordChangedCommand);
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.user;


        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp =  await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Users/{GlobalUserVariables.UserID}/Password", Data.Password);
                RequestIsWorking = false;
                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                }
                else
                {
                    SendExceptionMessage("Password konnte nicht gespeichert werden.");
                }
            }
        }

        private void ExecutePasswordChangedCommand(PasswordBox obj)
        {
            if (obj != null)
                Password = obj.Password;
        }
        #endregion

        #region Bindings
       
        public string Password
        {
            get => Data.Password;
            set
            {

                if (RequestIsWorking || !Equals(Data.Password, value))
                {
                    Data.Password = value;
                    ValidatePassword(value);
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    base.OnPropertyChanged();
                }
            }
        }

        public RelayCommand<PasswordBox> PasswordCommand { get; private set; }
        #endregion

        #region Validierung
        private bool ValidatePassword(string password)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(password, "Password", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Password", validationErrors);
            return isValid;
        }

        #endregion

        protected override void OnActivated()
        {
            Data = new UserModel();
            OnPropertyChanged();
            ValidatePassword("");
        }
    }
}
