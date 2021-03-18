using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Model.SchnurEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Data.Types.SchnurschiessenTypes;
using Vereinsverwaltung.Logic.Core.SchnurschiessenCore;
using Vereinsverwaltung.Logic.Core.Validierungen.Base;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;
using Vereinsverwaltung.Logic.UI.InterfaceViewModels;

namespace Vereinsverwaltung.Logic.UI.SchnurschiessenViewModels
{
    public class SchnurStammdatenViewModel : ViewModelStammdaten<Schnur>, IViewModelStammdaten
    {
        public SchnurStammdatenViewModel() : base(new SchnurAPI())
        {
            Title = "Schnur Stammdaten";
        }

        public void ZeigeStammdatenAn(int id)
        {
            var Schnur = new SchnurAPI().Lade(id);
            Bezeichnung = Schnur.Bezeichnung;
            Schnurtyp = Schnur.Schnurtyp;
            data = Schnur;
            state = State.Bearbeiten;
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
            data = new Schnur();
            Bezeichnung = "";
            Schnurtyp = Data.Types.SchnurschiessenTypes.Schnurtypes.schnur;
            state = State.Neu;
        }
    }
}
