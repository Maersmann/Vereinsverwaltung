using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Model.MitgliederEntitys;
using Vereinsverwaltung.Logic.Core;
using Vereinsverwaltung.Logic.Core.Validierungen;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.MitgliederViewModels
{
    public class MitgliederStammdatenViewModel : ViewModelStammdaten
    {
        private Mitglied mitglied;

        private int mitgliedsID { set { mitglied.ID = value; } } 

        public MitgliederStammdatenViewModel()
        {
            mitglied = new Mitglied();
            SaveCommand = new DelegateCommand(this.ExecuteSaveCommand, this.CanExecuteSaveCommand);
            ValidateEintrittsdatum(null);
            ValidateGeburtstag(null);
            ValidateName("");
            ValidateVorname("");
        }

        public void ZeigeMitglied(int inMitgliedID)
        {

            var Mitglied = new MitgliedAPI().Lade(inMitgliedID);
            Name = Mitglied.Name;
            Vorname = Mitglied.Vorname;
            Eintrittsdatum = Mitglied.Eintrittsdatum;
            Geburtstag = Mitglied.Geburtstag;
            Mitgliedsnr = Mitglied.Mitgliedsnr;
            Ort = Mitglied.Ort;
            Strasse = Mitglied.Straße;
            mitglied = Mitglied;    
        }

        #region Commands
        protected override void ExecuteSaveCommand()
        {
            var API = new MitgliedAPI();
            try
            {
                API.Speichern(mitglied);
                Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Mitglied gespeichert" });
            }
            catch (MitgliedMitMitgliedsNrVorhanden)

            {
                Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = false, Message = "Mitgliedsnr ist schon vorhanden" });
            }
        }

        protected override void ExecuteCloseCommand()
        {
            Cleanup();
        }
        #endregion

        #region Bindings
        public String Name
        {
            get { return mitglied.Name; }
            set
            {

                if ( !string.Equals(mitglied.Name, value))
                {
                    ValidateName(value);
                    mitglied.Name = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public String Vorname
        {
            get { return mitglied.Vorname; }
            set
            {

                if ( !string.Equals(mitglied.Vorname, value))
                {
                    ValidateVorname(value);
                    mitglied.Vorname = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public String Ort
        {
            get { return mitglied.Ort; }
            set
            {

                if ( !string.Equals(mitglied.Ort, value))
                {
                    mitglied.Ort = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public String Strasse
        {
            get { return mitglied.Straße; }
            set
            {

                if ( !string.Equals(mitglied.Straße, value))
                {
                    mitglied.Straße = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public int? Mitgliedsnr
        {
            get { return mitglied.Mitgliedsnr; }
            set
            {
                if ( !string.Equals(mitglied.Mitgliedsnr, value))
                {
                    mitglied.Mitgliedsnr = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public DateTime? Eintrittsdatum
        {
            get { return mitglied.Eintrittsdatum; }
            set
            {

                if ( !string.Equals(mitglied.Eintrittsdatum, value))
                {
                    ValidateEintrittsdatum(value);
                    mitglied.Eintrittsdatum = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public DateTime? Geburtstag
        {
            get { return mitglied.Geburtstag; }
            set
            {
                
                if ( !string.Equals(mitglied.Geburtstag, value))
                {
                    ValidateGeburtstag(value);
                    mitglied.Geburtstag = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Validierung
        private bool ValidateName(string inName)
        {
            var Validierung = new MitgliederStammdatenValidierung();

            bool isValid = Validierung.ValidateName(inName, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Name", validationErrors);
            return isValid;
        }
        private bool ValidateVorname(string inVorname)
        {
            var Validierung = new MitgliederStammdatenValidierung();

            bool isValid = Validierung.ValidateName(inVorname, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Vorname", validationErrors);

            return isValid;
        }
        private bool ValidateEintrittsdatum(DateTime? inEintrittsdatum)
        {
            var Validierung = new MitgliederStammdatenValidierung();

            bool isValid = Validierung.ValidateDatum(inEintrittsdatum, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Eintrittsdatum", validationErrors);

            return isValid;
        }
        private bool ValidateGeburtstag(DateTime? inGeburtstag)
        {
            var Validierung = new MitgliederStammdatenValidierung();

            bool isValid = Validierung.ValidateDatum(inGeburtstag, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Geburtstag", validationErrors);

            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            mitglied = new Mitglied();
            this.RaisePropertyChanged();
            ValidateEintrittsdatum(null);
            ValidateGeburtstag(null);
            ValidateName("");
            ValidateVorname("");
        }
    }
}
