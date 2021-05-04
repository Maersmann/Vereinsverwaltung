using Data.Model.SchnurrschiessenModels;
using Data.Types;
using Logic.Core.Validierungen.Base;
using Logic.UI.BaseViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurauszeichnungStammdatenViewModel : ViewModelStammdaten<SchnurauszeichnungStammdatenModel>, IViewModelStammdaten
    {
        public SchnurauszeichnungStammdatenViewModel()
        {
            Title = "Schnurauszeichung Stammdaten";
        }

        public void ZeigeStammdatenAn(int id)
        {
            // Todo: Request
            /*
            var Auszeichnung = new SchnurauszeichnungAPI().Lade(id);
            Bezeichnung = Auszeichnung.Bezeichnung;
            Rangfolge = Auszeichnung.Rangfolge;
            Hauptteil = Auszeichnung.Hauptteil;
            Zusatz = Auszeichnung.Zusatz;
            data = Auszeichnung;
            state = State.Bearbeiten;
            */
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnurauszeichnung;

        #region Commands
        protected override void ExecuteSaveCommand()
        {
            // Todo: Request
            /*
            data.HauptteilID = data.Hauptteil.ID;
            data.ZusatzID = data.Zusatz.ID;
            try
            {
                base.ExecuteSaveCommand();
            }
            catch (RangfolgeSchonVorhandenException)
            {
                SendExceptionMessage("Rangfolge ist schon vorhanden");
                return;
            }
            */
        }
        #endregion

        #region Bindings

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

        public int? Rangfolge
        {
            get => data.Rangfolge;
            set
            {
                if (!string.Equals(data.Rangfolge, value))
                {
                    ValidateZahl(value, "Rangfolge");
                    data.Rangfolge = value.GetValueOrDefault(0);
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        // Todo: Request
        /*
        public IEnumerable<Schnur> Schnuere => new SchnurAPI().LadeAlleSichtbaren();
        public IEnumerable<Schnur> SchnuereZusatz => new SchnurAPI().LadeAlle();
        
        public Schnur Hauptteil
        {
            get { return data.Hauptteil; }
            set
            {
                if (LoadAktie || (this.data.Hauptteil != value))
                {
                    this.data.Hauptteil = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public Schnur Zusatz
        {
            get { return data.Zusatz; }
            set
            {
                if (LoadAktie || (this.data.Zusatz != value))
                {
                    this.data.Zusatz = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        */
        #endregion

        #region Validierung

        private bool ValidateBezeichnung(string bezeichnung)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(bezeichnung, "", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Bezeichnung", validationErrors);
            return isValid;
        }

        private bool ValidateZahl(int? anzahl, string fieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateInteger(anzahl, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            data = new SchnurauszeichnungStammdatenModel();
            Bezeichnung = "";
            Rangfolge = null;
            state = State.Neu;
        }
    }
}
