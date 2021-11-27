using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.UserModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Controls;

namespace Logic.UI.UserViewModels
{
    public class UserStammdatenViewModel : ViewModelStammdaten<UserModel, StammdatenTypes>, IViewModelStammdaten
    {
        public UserStammdatenViewModel()
        {
            Title = "Stammdaten User";
            PasswordCommand = new RelayCommand<PasswordBox>(ExecutePasswordChangedCommand);
        }

        public async void ZeigeStammdatenAn(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Users/{id}");
                if (resp.IsSuccessStatusCode)
                    Response = await resp.Content.ReadAsAsync<Response<UserModel>>();
            }
            Name = Data.FirstName;
            Username = Data.Username;
            Password = Data.Password;
            Vorname = Data.FirstName;
            state = State.Bearbeiten;
            RequestIsWorking = false;
        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.user;
        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp;
                if (state == State.Neu)
                {
                    resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Users/register", Data);
                }
                else
                {
                    resp = await Client.PutAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Users/{Data.Id}", Data);
                }
                RequestIsWorking = false;
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else
                {
                    SendExceptionMessage("Benutzer konnte nicht gespeichert werden.");
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
        public string Name
        {
            get { return Data.LastName; }
            set
            {

                if (RequestIsWorking || !Equals(Data.LastName, value))
                {
                    Data.LastName = value;
                    base.RaisePropertyChanged();
                }
            }
        }
        public string Vorname
        {
            get => Data.FirstName;
            set
            {

                if (RequestIsWorking || !Equals(Data.FirstName, value))
                {
                    Data.FirstName = value;
                    base.RaisePropertyChanged();
                }
            }
        }
        public string Username
        {
            get => Data.Username;
            set
            {

                if (RequestIsWorking || !Equals(Data.Username, value))
                {
                    Data.Username = value;
                    ValidateUsername(value);
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    base.RaisePropertyChanged();
                }
            }
        }
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
                    base.RaisePropertyChanged();
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

        private bool ValidateUsername(string username)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(username, "Username", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Username", validationErrors);
            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            Data = new UserModel();
            RaisePropertyChanged();
            ValidatePassword("");
            ValidateUsername("");
            state = State.Neu;
        }
    }
}
