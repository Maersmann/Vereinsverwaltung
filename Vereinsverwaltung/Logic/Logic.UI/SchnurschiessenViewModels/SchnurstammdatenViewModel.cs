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
    public class SchnurstammdatenViewModel : ViewModelStammdaten<Schnur>, IViewModelStammdaten
    {
        public SchnurstammdatenViewModel()
        {
            Title = "Schnur Stammdaten";
        }

        public void ZeigeStammdatenAn(int id)
        {
            throw new NotImplementedException();
        }

        #region Commands
        protected override void ExecuteSaveCommand()
        {
            var API = new SchnurAPI();
            try
            {
                if (state.Equals(State.Neu))
                {
                    API.Speichern(data);
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Schnur gespeichert" }, StammdatenTypes.schnur);
                }
                else
                {
                    API.Aktualisieren(data);
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Schnur aktualisiert" }, StammdatenTypes.schnur);
                }
            }
            catch (Exception)
            {
                SendExceptionMessage("Nummer ist schon vorhanden");
                return;
            }
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.schnur);
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
