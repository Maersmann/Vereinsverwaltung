using Data.Model.MitgliederModels;
using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.Validierungen.Base;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.BaseMessages;
using Base.Logic.ViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Logic.Core;
using Base.Logic.Types;
using Base.Logic.Messages;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselbesitzerStammdatenViewModel : ViewModelStammdaten<SchluesselbesitzerModel, StammdatenTypes>, IViewModelStammdaten
    {
        public SchluesselbesitzerStammdatenViewModel() 
        {
            messageToken = "SchluesselbesitzerStammdaten";
            Title = "Schlüsselbesitzer Stammdaten";  
            MitgliedHinterlegenCommand = new RelayCommand(() => ExcecuteMitgliedHinterlegenCommand());
            DeleteMitgliedDataCommand = new DelegateCommand(this.ExecuteDeleteMitgliedDataCommand, this.CanExecuteDeleteMitgliedDataCommand);
        }

        public async void ZeigeStammdatenAn(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    data = await resp.Content.ReadAsAsync<SchluesselbesitzerModel>();
                }
            }

            Name = data.Name;
            ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
            RaisePropertyChanged("KeinMitgliedHinterlegt");
            RaisePropertyChanged("Mitgliedsnr");
            state = State.Bearbeiten;
            RequestIsWorking = false;


        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schluesselbesitzer;


        #region Bindings
        public string Name
        {
            get => data.Name;
            set
            {
                if (RequestIsWorking || !Equals(data.Name, value))
                {
                    ValidateName(value);
                    data.Name = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Mitgliedsnr
        { 
            get
            {
                return data.MitgliedsNr;
            }
        }
        public bool KeinMitgliedHinterlegt { get { return !data.MitgliedID.HasValue; } }
        public ICommand MitgliedHinterlegenCommand { get; set; }
        public ICommand DeleteMitgliedDataCommand { get; set; }
        #endregion

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer", data);
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if ((int)resp.StatusCode == 904)
                {
                    SendExceptionMessage("Mitglied ist schon vergeben");
                }
                else
                {
                    SendExceptionMessage("Besitzer konnte nicht gespeichert werden.");
                }
            }
        }

        private void ExcecuteMitgliedHinterlegenCommand()
        {
            Messenger.Default.Send<OpenMitgliedAuswahlMessage>(new OpenMitgliedAuswahlMessage(OpenMitgliedAuswahlCallback), messageToken);
        }

        private async void OpenMitgliedAuswahlCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    RequestIsWorking = true;
                    HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer/Mitglied/{id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        if (await resp.Content.ReadAsAsync<bool>())
                        { 
                            SendInformationMessage("Mitglied hat schon ein Datensatz");
                            RequestIsWorking = false;
                            return;
                        }
                    }
                    
                    resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder/{id}");
             
                    if (resp.IsSuccessStatusCode)
                    {
                        MitgliederModel Mitglied = await resp.Content.ReadAsAsync<MitgliederModel>();
                        data.MitgliedID = id;     
                        Name = Mitglied.Vorname + " " + Mitglied.Name;
                        data.MitgliedsNr = Mitglied.Mitgliedsnr;
                        ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
                        RaisePropertyChanged("KeinMitgliedHinterlegt");
                        RaisePropertyChanged("Mitgliedsnr");
                    }
                    RequestIsWorking = false;
                }
            }
        }

        private bool CanExecuteDeleteMitgliedDataCommand()
        {
            return !KeinMitgliedHinterlegt;
        }

        private void ExecuteDeleteMitgliedDataCommand()
        {
            data.MitgliedID = null;
            ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
            this.RaisePropertyChanged(nameof(Mitgliedsnr));
            Name = "";
            this.RaisePropertyChanged("KeinMitgliedHinterlegt");
        }

        #endregion

        #region Validierung
        private bool ValidateName(string name)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(name, "" , out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Name", validationErrors);
            return isValid;
        }
        #endregion


        public override void Cleanup()
        {
            data = new SchluesselbesitzerModel { };
            Name = "";
            state = State.Neu;
        }
    }
}
