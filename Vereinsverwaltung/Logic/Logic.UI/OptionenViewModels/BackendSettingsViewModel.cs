using Data.Model.OptionenModels;
using Data.Types.OptionTypes;
using GalaSoft.MvvmLight.Command;
using Logic.Core;
using Logic.Core.OptionenLogic;
using Logic.UI.BaseViewModels;
using Logic.UI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

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
            var backendLogic = new BackendLogic();
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
        public String Backend_IP
        {
            get
            {
                return model.Backend_IP;
            }
            set
            {
                model.Backend_IP = value;
                this.RaisePropertyChanged();
            }
        }
        public string Backend_URL
        {
            get
            {
                return model.Backend_URL;
            }
            set
            {
                model.Backend_URL = value;
                this.RaisePropertyChanged();
            }
        }
        public string Port
        {
            get
            {
                if (model.Port.HasValue)
                    return model.Port.Value.ToString();
                else
                    return "";
            }
            set
            {
                if (value.Equals(""))
                    this.model.Port = null;
                else
                    this.model.Port = int.Parse(value);
                this.RaisePropertyChanged();
            }
        }

        public IEnumerable<BackendProtokollTypes> BackendProtokollTypes => Enum.GetValues(typeof(BackendProtokollTypes)).Cast<BackendProtokollTypes>();
        public BackendProtokollTypes BackendProtokollTyp
        {
            get { return model.ProtokollTyp; }
            set
            {
                this.model.ProtokollTyp = value;
                this.RaisePropertyChanged();
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
