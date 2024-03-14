using Data.Model.MitgliederModels;
using Data.Types;
using Data.Types.MitgliederTypes;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Base.Logic.ViewModels;
using Base.Logic.Core;
using Base.Logic.Types;
using Base.Logic.Messages;
using Base.Logic.Wrapper;

namespace Logic.UI.MitgliederViewModels
{
    public class MitgliederStammdatenViewModel : ViewModelStammdaten<MitgliederModel, StammdatenTypes>, IViewModelStammdaten
    {
        public MitgliederStammdatenViewModel() 
        {
            Title = "Stammdaten Mitglied";
        }

        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder/{id}");
                if (resp.IsSuccessStatusCode)
                    Response = await resp.Content.ReadAsAsync<Response<MitgliederModel>>();
            }
            Name = Data.Name;
            Eintrittsdatum = Data.Eintrittsdatum;
            Austrittsdatum = Data.Austrittsdatum;
            Geburtstag = Data.Geburtstag;
            Mitgliedsnr = Data.Mitgliedsnr;
            Ort = Data.Ort;
            Strasse = Data.Straße;
            Vorname = Data.Vorname;
            Geschlecht = Data.Geschlecht;
            state = State.Bearbeiten;
            RequestIsWorking = false;
        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.mitglied;
        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Mitglieder", Data);
                RequestIsWorking = false;
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if ((int)resp.StatusCode == 902)
                {
                    SendExceptionMessage("MitgliedsNr ist schon vergeben");
                }
                else
                {
                    SendExceptionMessage("Mitglied konnte nicht gespeichert werden.");
                }           
            }
        }
        #endregion

        #region Bindings
        public string Name
        {
            get { return Data.Name; }
            set
            {

                if (RequestIsWorking || !Equals(Data.Name, value))
                {
                    ValidateName(value);
                    Data.Name = value;
                    base.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public string Vorname
        {
            get => Data.Vorname;
            set
            {

                if (RequestIsWorking || !Equals(Data.Vorname, value))
                {
                    ValidateVorName(value);
                    Data.Vorname = value;
                    base.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public string Ort
        {
            get => Data.Ort;
            set
            {

                if (RequestIsWorking || !Equals(Data.Ort, value))
                {
                    Data.Ort = value;
                    base.RaisePropertyChanged();
                }
            }
        }
        public string Strasse
        {
            get => Data.Straße;
            set
            {

                if (RequestIsWorking || !Equals(Data.Straße, value))
                {
                    Data.Straße = value;
                    base.RaisePropertyChanged();
                }
            }
        }
        public int? Mitgliedsnr
        {
            get => Data.Mitgliedsnr;
            set
            {
                if (RequestIsWorking || !Equals(Data.Mitgliedsnr, value))
                {
                    Data.Mitgliedsnr = value;
                    base.RaisePropertyChanged();
                }
            }
        }
        public DateTime? Eintrittsdatum
        {
            get => Data.Eintrittsdatum;
            set
            {

                if (RequestIsWorking || !Equals(Data.Eintrittsdatum, value))
                {
                    ValidateEintrittsdatum(value);
                    Data.Eintrittsdatum = value;
                    base.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public DateTime? Austrittsdatum
        {
            get => Data.Austrittsdatum;
            set
            {

                if (RequestIsWorking || !Equals(Data.Austrittsdatum, value))
                {
                    Data.Austrittsdatum = value;
                    base.RaisePropertyChanged();
                }
            }
        }
        public DateTime? Geburtstag
        {
            get => Data.Geburtstag;
            set
            {

                if (RequestIsWorking || !Equals(Data.Geburtstag, value))
                {
                    ValidateGeburtstag(value);
                    Data.Geburtstag = value;
                    base.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public IEnumerable<Geschlecht> Geschlechter => Enum.GetValues(typeof(Geschlecht)).Cast<Geschlecht>();
        public Geschlecht Geschlecht
        {
            get => Data.Geschlecht;
            set
            {
                if (RequestIsWorking || (Data.Geschlecht != value))
                {
                    Data.Geschlecht = value;
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
            Data = new MitgliederModel();
            RaisePropertyChanged();
            ValidateEintrittsdatum(null);
            ValidateGeburtstag(null);
            ValidateName("");
            ValidateVorName(""); 
            state = State.Neu;
        }
    }
}
