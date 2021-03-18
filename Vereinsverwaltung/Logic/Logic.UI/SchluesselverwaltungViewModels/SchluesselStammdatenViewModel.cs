using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Core.SchluesselCore;
using Vereinsverwaltung.Logic.Core.SchluesselCore.Exceptions;
using Vereinsverwaltung.Logic.Core.Validierungen.Base;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;
using Vereinsverwaltung.Logic.UI.InterfaceViewModels;

namespace Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselStammdatenViewModel : ViewModelStammdaten<Schluessel>, IViewModelStammdaten
    {
        public SchluesselStammdatenViewModel() : base(new SchluesselAPI())
        {
            Title = "Schlüssel Stammdaten";
        }

        public void ZeigeStammdatenAn(int id)
        {
            var Schluessel = new SchluesselAPI().Lade(id);
            Nummer = Schluessel.Nummer;
            Beschreibung = Schluessel.Beschreibung;
            Bezeichnung = Schluessel.Bezeichnung;
            Bestand = Schluessel.Bestand;
            data = Schluessel;
            state = State.Bearbeiten;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schluessel;

        #region Commands
        protected override void ExecuteSaveCommand()
        {
            try
            {
                base.ExecuteSaveCommand();
            }
            catch (SchluesselNummerIstSchonVorhandenException)
            {
                SendExceptionMessage("Nummer ist schon vorhanden");
                return;
            }
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
            data = new Schluessel();
            Nummer = null;
            Bestand = null;
            Beschreibung = "";
            Bezeichnung = "";
            state = State.Neu;
        }

    }
}
