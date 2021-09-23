using Data.Model.OptionenModels;
using Data.Types.OptionTypes;
using GalaSoft.MvvmLight.Command;
using Logic.Core;
using Logic.Core.OptionenLogic;
using Base.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Base.Logic.Core;

namespace Logic.UI.OptionenViewModels
{
    public class BackendSettingsViewModel : ViewModelBasis
    {
        private readonly BackendSettingsModel model;
        public BackendSettingsViewModel()
        {
            model = new BackendSettingsModel();
            SpeicherSettingsCommand = new RelayCommand(() => ExecuteSpeicherSettingsCommand());
            TestConnectionCommand = new RelayCommand(() => ExecuteTestConnectionCommand());
            Title = "Backend-Settings";
            SetModelData();
        }

        public void SetModelData()
        {
            BackendLogic backendLogic = new BackendLogic();
            if (backendLogic.IstINIVorhanden())
            {
                backendLogic.LoadData();
                model.Backend_IP = backendLogic.GetBackendIP();
                model.ProtokollTyp = backendLogic.GetProtokollTyp();
                model.Port = backendLogic.GetBackendPort();
                model.Backend_URL = backendLogic.GetBackendURL();
            }
        }

        #region Bindings
        public ICommand SpeicherSettingsCommand { get; set; }
        public ICommand TestConnectionCommand { get; set; }
        public string Backend_IP
        {
            get => model.Backend_IP;
            set
            {
                model.Backend_IP = value;
                RaisePropertyChanged();
            }
        }
        public string Backend_URL
        {
            get => model.Backend_URL;
            set
            {
                model.Backend_URL = value;
                RaisePropertyChanged();
            }
        }
        public string Port
        {
            get => model.Port.HasValue ? model.Port.Value.ToString() : "";
            set
            {
                model.Port = value.Equals("") ? null : (int?)int.Parse(value);
                RaisePropertyChanged();
            }
        }

        public IEnumerable<BackendProtokollTypes> BackendProtokollTypes => Enum.GetValues(typeof(BackendProtokollTypes)).Cast<BackendProtokollTypes>();
        public BackendProtokollTypes BackendProtokollTyp
        {
            get => model.ProtokollTyp;
            set
            {
                model.ProtokollTyp = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands
        private void ExecuteSpeicherSettingsCommand()
        {
            var backendlogic = new BackendLogic();
            backendlogic.SaveData(model.Backend_IP, model.ProtokollTyp, model.Port, model.Backend_URL);
            SendInformationMessage("Settings gespeichert");
            GlobalVariables.BackendServer_IP = backendlogic.GetBackendIP();
            GlobalVariables.BackendServer_URL = backendlogic.GetURL();
            GlobalVariables.BackendServer_Port = backendlogic.GetBackendPort();
            new BackendHelper().CheckServerIsOnline();
            ViewModelLocator locator = new ViewModelLocator();
            locator.Main.RaisePropertyChanged("MenuIsEnabled");
        }

        private void ExecuteTestConnectionCommand()
        {
            bool isOnline;
            if (model.Port.HasValue)
                isOnline = new BackendHelper().TestCheckServerIsOnline(model.Backend_IP, model.Port.Value);
            else
                isOnline = new BackendHelper().TestCheckServerIsOnline(model.Backend_IP);

            if (isOnline)
            {
                SendInformationMessage("Test Verbindung erfolgreich");
            }
        }
        #endregion
    }
}
