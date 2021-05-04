using Data.Model.MitgliederModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen;
using Logic.UI.BaseViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.MitgliederViewModels
{
    public class MitgliederStammdatenViewModel : ViewModelStammdaten<MitgliedStammdatenModel>, IViewModelStammdaten
    {


        public MitgliederStammdatenViewModel() 
        {
            Title = "Stammdaten Mitglied";
        }

        public void ZeigeStammdatenAn(int id)
        {
            // Todo: Request
            /*
            var Mitglied = new MitgliedAPI().Lade(id);
            Name = Mitglied.Name;
            Eintrittsdatum = Mitglied.Eintrittsdatum;
            Geburtstag = Mitglied.Geburtstag;
            Mitgliedsnr = Mitglied.Mitgliedsnr;
            Ort = Mitglied.Ort;
            Strasse = Mitglied.Straße;
            Vorname = Mitglied.Vorname;
            data = Mitglied;
            state = State.Bearbeiten;
            */
        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.mitglied;
        #region Commands
        protected override void ExecuteSaveCommand()
        {
            // Todo: Request
            /*
            try
            {
                base.ExecuteSaveCommand();
            }
            catch (MitgliedMitMitgliedsNrVorhanden)
            {
                SendExceptionMessage("Mitgliedsnr ist schon vorhanden");
                return;
            }
            */
        }
        #endregion

        #region Bindings
        public String Name
        {
            get { return data.Name; }
            set
            {

                if ( !string.Equals(data.Name, value))
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

                if (!string.Equals(data.Vorname, value))
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

                if ( !string.Equals(data.Ort, value))
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

                if ( !string.Equals(data.Straße, value))
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
                if ( !string.Equals(data.Mitgliedsnr, value))
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

                if ( !string.Equals(data.Eintrittsdatum, value))
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
                
                if ( !string.Equals(data.Geburtstag, value))
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
            data = new MitgliedStammdatenModel();
            this.RaisePropertyChanged();
            ValidateEintrittsdatum(null);
            ValidateGeburtstag(null);
            ValidateName("");
            ValidateVorName(""); 
            state = State.Neu;
        }
    }
}
