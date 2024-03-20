using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.UserModels;
using Data.Types;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Prism.Commands;

namespace Logic.UI.UserViewModels
{
    public class UserBerechtigungenUebersichtViewModel : ViewModelUebersicht<UserBerechtigungModel, StammdatenTypes>
    {
        private ObservableCollection<BerechtigungTypes> berechtigungen;
        private BerechtigungTypes selectedBerechtigungItem;
        private IList<UserBerechtigungModel> userBerechtigungen;
        private UserBerechtigungModel selectedUserBerechtigungItem;
        private int userID;
        public UserBerechtigungenUebersichtViewModel()
        {
            userBerechtigungen = [];
            berechtigungen = [];
            userID = 0;
            Title = "Übersicht Userberechtigungen";
            BerechtigungEntfernenCommand = new RelayCommand(() => new DelegateCommand(ExecuteBerechtigungEntfernenCommand, CanPost));
            BerechtigungHinzufuegenCommand = new RelayCommand(() => new DelegateCommand(ExecuteBerechtigungHinzufuegenCommand, CanPost));
        }

        protected override int GetID() { return SelectedItem.ID; }
        protected override string GetREST_API() { return $"/api/UserBerechtigung/{userID}"; }
        protected override bool LoadingOnCreate() => false;

        public int UserID
        {
            private get => userID;
            set
            {
                userID = value;
                LoadBerechtigungen();
            }
            
        }

        public async void LoadBerechtigungen()
        {
            RequestIsWorking = true;
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Berechtigung/NichtVergebene?UserID={userID}");
            if (resp.IsSuccessStatusCode)
            {
                berechtigungen = await resp.Content.ReadAsAsync<ObservableCollection<BerechtigungTypes>>();
                if (Berechtigungen.Count > 0)
                {
                    SelectedBerechtigungItem = Berechtigungen.ElementAt(0);
                }
            }

            resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + GetREST_API());
            if (resp.IsSuccessStatusCode)
            {
                userBerechtigungen = await resp.Content.ReadAsAsync<IList<UserBerechtigungModel>>();
                if (UserBerechtigungen.Count > 0)
                {
                    SelectedUserBerechtigungItem = UserBerechtigungen.ElementAt(0);
                }
            }
            OnPropertyChanged(nameof(Berechtigungen));
            OnPropertyChanged(nameof(UserBerechtigungen));
            RequestIsWorking = false;
        }

        #region Bindings

        public ICommand BerechtigungEntfernenCommand { get; set; }
        public ICommand BerechtigungHinzufuegenCommand { get; set; }

        public IList<UserBerechtigungModel> UserBerechtigungen => userBerechtigungen;
        public UserBerechtigungModel SelectedUserBerechtigungItem
        {
            get => selectedUserBerechtigungItem;
            set
            {
                selectedUserBerechtigungItem = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BerechtigungTypes> Berechtigungen => berechtigungen;
        public BerechtigungTypes SelectedBerechtigungItem
        {
            get => selectedBerechtigungItem;
            set
            {
                selectedBerechtigungItem = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands

        protected async void ExecuteBerechtigungEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                _ = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/UserBerechtigung/{SelectedUserBerechtigungItem.ID}");

                SendInformationMessage("Berechtigung gelöscht");
                RequestIsWorking = false;
                UserID = userID;
            }
        }
        protected async void ExecuteBerechtigungHinzufuegenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                var Berechtigung = new UserBerechtigungModel { UserID = UserID, Berechtigung = SelectedBerechtigungItem };
                RequestIsWorking = true;
                _ = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/UserBerechtigung", Berechtigung);

                SendInformationMessage("Berechtigung hinzugefügt");
                RequestIsWorking = false;
                UserID = userID;
            }
        }
        public bool CanPost() => !RequestIsWorking;
        #endregion

        public override bool RequestIsWorking
        {
            get => base.RequestIsWorking;
            set
            {
                base.RequestIsWorking = value;
                if (BerechtigungEntfernenCommand != null)
                {
                    ((DelegateCommand)BerechtigungEntfernenCommand).RaiseCanExecuteChanged();
                }
                if (BerechtigungHinzufuegenCommand != null)
                {
                    ((DelegateCommand)BerechtigungHinzufuegenCommand).RaiseCanExecuteChanged();
                }
            }
        }
    }
}
