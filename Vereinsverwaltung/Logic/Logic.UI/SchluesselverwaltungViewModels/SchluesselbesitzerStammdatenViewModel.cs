using Data.Model.MitgliederModels;
using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.BaseMessages;
using Base.Logic.ViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using Base.Logic.Core;
using Base.Logic.Types;
using Base.Logic.Messages;
using Base.Logic.Wrapper;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselbesitzerStammdatenViewModel : ViewModelStammdaten<SchluesselbesitzerModel, StammdatenTypes>, IViewModelStammdaten
    {
        public SchluesselbesitzerStammdatenViewModel() 
        {
            messageToken = "SchluesselbesitzerStammdaten";
            Title = "Schlüsselbesitzer Stammdaten";  
            MitgliedHinterlegenCommand = new RelayCommand(() => ExcecuteMitgliedHinterlegenCommand());
            DeleteMitgliedDataCommand = new DelegateCommand(ExecuteDeleteMitgliedDataCommand, CanExecuteDeleteMitgliedDataCommand);
        }

        public async void ZeigeStammdatenAn(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    Response = await resp.Content.ReadAsAsync<Response<SchluesselbesitzerModel>>();
                }
            }

            Name = Data.Name;
            ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(KeinMitgliedHinterlegt));
            RaisePropertyChanged(nameof(Mitgliedsnr));
            state = State.Bearbeiten;
            RequestIsWorking = false;

        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schluesselbesitzer;


        #region Bindings
        public string Name
        {
            get => Data.Name;
            set
            {
                if (RequestIsWorking || !Equals(Data.Name, value))
                {
                    ValidateName(value);
                    Data.Name = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Mitgliedsnr => Data.MitgliedsNr;
        public bool KeinMitgliedHinterlegt { get { return !Data.MitgliedID.HasValue; } }
        public ICommand MitgliedHinterlegenCommand { get; set; }
        public ICommand DeleteMitgliedDataCommand { get; set; }
        #endregion

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer", Data);
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
            Messenger.Default.Send(new OpenMitgliedAuswahlMessage(OpenMitgliedAuswahlCallback), messageToken);
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
                        Response<MitgliederModel> Mitglied = await resp.Content.ReadAsAsync<Response<MitgliederModel>>();
                        Data.MitgliedID = id;     
                        Name = Mitglied.Data.Vorname + " " + Mitglied.Data.Name;
                        Data.MitgliedsNr = Mitglied.Data.Mitgliedsnr;
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
            Data.MitgliedID = null;
            Data.MitgliedsNr = null;
            ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(Mitgliedsnr));
            Name = "";
            RaisePropertyChanged("KeinMitgliedHinterlegt");
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
            Data = new SchluesselbesitzerModel { };
            Name = "";
            state = State.Neu;
        }
    }
}
