using Data.Model.MitgliederModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.Validierungen;
using Logic.Messages.BaseMessages;
using Logic.UI.BaseViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.MitgliederViewModels
{
    public class MitgliederStammdatenViewModel : ViewModelStammdaten<MitgliederModel>, IViewModelStammdaten
    {


        public MitgliederStammdatenViewModel() 
        {
            Title = "Stammdaten Mitglied";
        }

        public async void ZeigeStammdatenAn(int id)
        {
            LoadData = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder/{id}");
                if (resp2.IsSuccessStatusCode)
                    data = await resp2.Content.ReadAsAsync<MitgliederModel>();
            }
            Name = data.Name;
            Eintrittsdatum = data.Eintrittsdatum;
            Geburtstag = data.Geburtstag;
            Mitgliedsnr = data.Mitgliedsnr;
            Ort = data.Ort;
            Strasse = data.Straße;
            Vorname = data.Vorname;
            state = State.Bearbeiten;
            LoadData = false;
        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.mitglied;
        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder", data);

                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), GetStammdatenTyp());
                }
                else if ((int)resp.StatusCode == 902)
                {
                    SendExceptionMessage("MitgliedsNr ist schon vergeben");
                    return;
                }
                else
                {
                    SendExceptionMessage("Fehler beim Speichern!" + Environment.NewLine + resp.StatusCode);
                    return;
                }
            }
        }
        #endregion

        #region Bindings
        public String Name
        {
            get { return data.Name; }
            set
            {

                if (LoadData || !string.Equals(data.Name, value))
                {
                    ValidateName(value);
                    data.Name = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public String Vorname
        {
            get { return data.Vorname; }
            set
            {

                if (LoadData || !string.Equals(data.Vorname, value))
                {
                    ValidateVorName(value);
                    data.Vorname = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public String Ort
        {
            get { return data.Ort; }
            set
            {

                if (LoadData || !string.Equals(data.Ort, value))
                {
                    data.Ort = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public String Strasse
        {
            get { return data.Straße; }
            set
            {

                if (LoadData || !string.Equals(data.Straße, value))
                {
                    data.Straße = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public int? Mitgliedsnr
        {
            get { return data.Mitgliedsnr; }
            set
            {
                if (LoadData || !string.Equals(data.Mitgliedsnr, value))
                {
                    data.Mitgliedsnr = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public DateTime? Eintrittsdatum
        {
            get { return data.Eintrittsdatum; }
            set
            {

                if (LoadData || !string.Equals(data.Eintrittsdatum, value))
                {
                    ValidateEintrittsdatum(value);
                    data.Eintrittsdatum = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public DateTime? Geburtstag
        {
            get { return data.Geburtstag; }
            set
            {
                
                if (LoadData || !string.Equals(data.Geburtstag, value))
                {
                    ValidateGeburtstag(value);
                    data.Geburtstag = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Validierung
        private bool ValidateName(string name)
        {
            var Validierung = new MitgliederStammdatenValidierung();

            bool isValid = Validierung.ValidateString(name, "" , out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Name", validationErrors);
            return isValid;
        }

        private bool ValidateVorName(string name)
        {
            var Validierung = new MitgliederStammdatenValidierung();

            bool isValid = Validierung.ValidateString(name, "", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Vorname", validationErrors);
            return isValid;
        }

        private bool ValidateEintrittsdatum(DateTime? eintrittsdatum)
        {
            var Validierung = new MitgliederStammdatenValidierung();

            bool isValid = Validierung.ValidateDatum(eintrittsdatum, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Eintrittsdatum", validationErrors);

            return isValid;
        }
        private bool ValidateGeburtstag(DateTime? geburtstag)
        {
            var Validierung = new MitgliederStammdatenValidierung();

            bool isValid = Validierung.ValidateDatum(geburtstag, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Geburtstag", validationErrors);

            return isValid;
        }
        #endregion
        
        public override void Cleanup()
        {
            data = new MitgliederModel();
            this.RaisePropertyChanged();
            ValidateEintrittsdatum(null);
            ValidateGeburtstag(null);
            ValidateName("");
            ValidateVorName(""); 
            state = State.Neu;
        }
    }
}
