using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.UI.BaseViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselStammdatenViewModel : ViewModelStammdaten<SchluesselStammdatenModel>, IViewModelStammdaten
    {
        public SchluesselStammdatenViewModel() 
        {
            Title = "Schlüssel Stammdaten";
        }

        public void ZeigeStammdatenAn(int id)
        {
            // Todo: Request
            /*
            var Schluessel = new SchluesselAPI().Lade(id);
            Nummer = Schluessel.Nummer;
            Beschreibung = Schluessel.Beschreibung;
            Bezeichnung = Schluessel.Bezeichnung;
            Bestand = Schluessel.Bestand;
            data = Schluessel;
            state = State.Bearbeiten;
            */
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schluessel;

        #region Commands
        protected override void ExecuteSaveCommand()
        {
            //Todo: Request
            /*
            try
            {
                base.ExecuteSaveCommand();
            }
            catch (SchluesselNummerIstSchonVorhandenException)
            {
                SendExceptionMessage("Nummer ist schon vorhanden");
                return;
            }
            */
        }
        #endregion

        #region Bindings
        public int? Nummer 
        {
            get
            {
                return data.Nummer;
            }
            set
            {
                if (!string.Equals(data.Nummer, value))
                {
                    ValidateAnzahl(value, "Nummer");
                    data.Nummer = value.GetValueOrDefault();
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public String Beschreibung
        {
            get
            {
                return data.Beschreibung;
            }
            set
            {
                if (!string.Equals(data.Beschreibung, value))
                {
                    data.Beschreibung = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public String Bezeichnung
        {
            get
            {
                return data.Bezeichnung;
            }
            set
            {
                if (!string.Equals(data.Bezeichnung, value))
                {
                    ValidateBezeichnung(value);
                    data.Bezeichnung = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Bestand
        {
            get
            {
                return data.Bestand;
            }
            set
            {
                if (!string.Equals(data.Bestand, value))
                {
                    data.Bestand = value.GetValueOrDefault(0);
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Validierung
        private bool ValidateAnzahl(int? anzahl, string fieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateInteger(anzahl, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }
        private bool ValidateBezeichnung(string bezeichnung)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(bezeichnung, "", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Bezeichnung", validationErrors);
            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            data = new SchluesselStammdatenModel();
            Nummer = null;
            Bestand = null;
            Beschreibung = "";
            Bezeichnung = "";
            state = State.Neu;
        }

    }
}
