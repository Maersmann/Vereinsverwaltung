using Data.Model.MitgliederModels;
using Data.Types;
using Data.Types.MitgliederTypes;
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
            DataIsLoading = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder/{id}");
                if (resp.IsSuccessStatusCode)
                    data = await resp.Content.ReadAsAsync<MitgliederModel>();
            }
            Name = data.Name;
            Eintrittsdatum = data.Eintrittsdatum;
            Austrittsdatum = data.Austrittsdatum;
            Geburtstag = data.Geburtstag;
            Mitgliedsnr = data.Mitgliedsnr;
            Ort = data.Ort;
            Strasse = data.Straße;
            Vorname = data.Vorname;
            Geschlecht = data.Geschlecht;
            state = State.Bearbeiten;
            DataIsLoading = false;
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
                    SendExceptionMessage("Mitglied konnte nicht gespeichert werden.");
                    return;
                }
            }
        }
        #endregion

        #region Bindings
        public string Name
        {
            get { return data.Name; }
            set
            {

                if (DataIsLoading || !Equals(data.Name, value))
                {
                    ValidateName(value);
                    data.Name = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string Vorname
        {
            get => data.Vorname;
            set
            {

                if (DataIsLoading || !Equals(data.Vorname, value))
                {
                    ValidateVorName(value);
                    data.Vorname = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string Ort
        {
            get => data.Ort;
            set
            {

                if (DataIsLoading || !Equals(data.Ort, value))
                {
                    data.Ort = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Strasse
        {
            get => data.Straße;
            set
            {

                if (DataIsLoading || !Equals(data.Straße, value))
                {
                    data.Straße = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int? Mitgliedsnr
        {
            get => data.Mitgliedsnr;
            set
            {
                if (DataIsLoading || !Equals(data.Mitgliedsnr, value))
                {
                    data.Mitgliedsnr = value;
                    RaisePropertyChanged();
                }
            }
        }
        public DateTime? Eintrittsdatum
        {
            get => data.Eintrittsdatum;
            set
            {

                if (DataIsLoading || !Equals(data.Eintrittsdatum, value))
                {
                    ValidateEintrittsdatum(value);
                    data.Eintrittsdatum = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime? Austrittsdatum
        {
            get => data.Austrittsdatum;
            set
            {

                if (DataIsLoading || !Equals(data.Austrittsdatum, value))
                {
                    data.Austrittsdatum = value;
                    RaisePropertyChanged();
                }
            }
        }
        public DateTime? Geburtstag
        {
            get => data.Geburtstag;
            set
            {

                if (DataIsLoading || !Equals(data.Geburtstag, value))
                {
                    ValidateGeburtstag(value);
                    data.Geburtstag = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public IEnumerable<Geschlecht> Geschlechter
        {
            get
            {
                return Enum.GetValues(typeof(Geschlecht)).Cast<Geschlecht>();
            }
        }
        public Geschlecht Geschlecht
        {
            get => data.Geschlecht;
            set
            {
                if (DataIsLoading || (data.Geschlecht != value))
                {
                    data.Geschlecht = value;
                    RaisePropertyChanged();
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
            RaisePropertyChanged();
            ValidateEintrittsdatum(null);
            ValidateGeburtstag(null);
            ValidateName("");
            ValidateVorName(""); 
            state = State.Neu;
        }
    }
}
