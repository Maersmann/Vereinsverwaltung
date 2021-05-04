using Data.Model.SchnurrschiessenModels;
using Data.Types;
using Data.Types.SchnurschiessenTypes;
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

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurStammdatenViewModel : ViewModelStammdaten<SchnurstammdatenModel>, IViewModelStammdaten
    {
        public SchnurStammdatenViewModel()
        {
            Title = "Schnur Stammdaten";
        }

        public void ZeigeStammdatenAn(int id)
        {
            // Todo: Request
            /*
            var Schnur = new SchnurAPI().Lade(id);
            Bezeichnung = Schnur.Bezeichnung;
            Schnurtyp = Schnur.Schnurtyp;
            data = Schnur;
            state = State.Bearbeiten;
            */
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnur;

        #region Commands
        protected override void ExecuteSaveCommand()
        {
            try
            {
                base.ExecuteSaveCommand();       
            }
            catch (Exception)
            {
                SendExceptionMessage("Nummer ist schon vorhanden");
                return;
            }
           
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

        public IEnumerable<Schnurtypes> Schnurtypes
        {
            get
            {
                return Enum.GetValues(typeof(Schnurtypes)).Cast<Schnurtypes>();
            }
        }
        public Schnurtypes Schnurtyp
        {
            get { return data.Schnurtyp; }
            set
            {
                if (LoadAktie || (this.data.Schnurtyp != value))
                {
                    this.data.Schnurtyp = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }


        #endregion

        #region Validierung

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
            data = new SchnurstammdatenModel();
            Bezeichnung = "";
            Schnurtyp = Data.Types.SchnurschiessenTypes.Schnurtypes.schnur;
            state = State.Neu;
        }
    }
}
