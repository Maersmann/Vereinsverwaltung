using Data.Model.MitgliederModels;
using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.Validierungen.Base;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.BaseMessages;
using Logic.UI.BaseViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselbesitzerStammdatenViewModel : ViewModelStammdaten<SchluesselbesitzerModel>, IViewModelStammdaten
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
            LoadData = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer/{id}");
                if (resp2.IsSuccessStatusCode)
                    data = await resp2.Content.ReadAsAsync<SchluesselbesitzerModel>();
            }

            Name = data.Name;
            ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
            this.RaisePropertyChanged("KeinMitgliedHinterlegt");
            this.RaisePropertyChanged("Mitgliedsnr");
            state = State.Bearbeiten;
            
        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schluesselbesitzer;


        #region Bindings
        public String Name 
        { 
            get
            {
                return data.Name;
            }
            set
            {
                if( LoadData || !string.Equals(data.Name,value))
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
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer", data);


                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), GetStammdatenTyp());
                }
                else
                {
                    SendExceptionMessage("Fehler: Speicher Besitzer" + Environment.NewLine+ resp.StatusCode);
                    return;
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
                    HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/besitzer/Mitglied/{id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        if (await resp.Content.ReadAsAsync<bool>())
                        { 
                            this.SendInformationMessage("Mitglied hat schon ein Datensatz");
                            return;
                        }
                    }
                    
                    resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder/{id}");
             
                    if (resp.IsSuccessStatusCode)
                    { 
                        var Mitglied = await resp.Content.ReadAsAsync<MitgliederModel>();
                        data.MitgliedID = id;          
                        Name = Mitglied.Vorname + " " + Mitglied.Name;
                        data.MitgliedsNr = Mitglied.Mitgliedsnr;
                        ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
                        this.RaisePropertyChanged("KeinMitgliedHinterlegt");
                        this.RaisePropertyChanged("Mitgliedsnr");
                    }
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
            this.RaisePropertyChanged("Mitgliedsnr");
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
