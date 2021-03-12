using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Data.Types.SchluesselverwaltungTypes;
using Vereinsverwaltung.Logic.Core.SchluesselCore;
using Vereinsverwaltung.Logic.Core.SchluesselCore.Exceptions;
using Vereinsverwaltung.Logic.Core.Validierungen.Base;
using Vereinsverwaltung.Logic.Messages.AuswahlMessages;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;
using Vereinsverwaltung.Logic.UI.InterfaceViewModels;

namespace Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselzuteilungStammdatenViewModel : ViewModelStammdaten<Schluesselzuteilung>, ISchluesselzuteilungStammdatenViewModel
    {
        private SchluesselzuteilungTypes auswahlTypes;

        public SchluesselzuteilungStammdatenViewModel()
        {
            Title = "Schlüsselzuteilung";
            OpenAuswahlSchluesselCommand = new DelegateCommand(this.ExecuteOpenAuswahlSchluesselCommand, this.CanExecuteOpenAuswahlSchluesselCommand);
            OpenAuswahlBesitzerCommand = new DelegateCommand(this.ExecuteOpenAuswahlBesitzerCommand, this.CanOpenAuswahlBesitzerCommand);
        }

        public void BySchluesselID(int schluesselID)
        {
            data.SchluesselID = schluesselID;
            data.Schluessel = new SchluesselAPI().Lade(schluesselID);
            auswahlTypes = SchluesselzuteilungTypes.Besitzer;
            ((DelegateCommand)OpenAuswahlSchluesselCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)OpenAuswahlBesitzerCommand).RaiseCanExecuteChanged();
            ValidateBezeichnung(Schluesselbesitzer, "Schluesselbesitzer", "Besitzer");
            this.RaisePropertyChanged("SchluesselBez");
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public void BySchluesselbesitzerID(int schluesselbesitzerID)
        {
            data.SchluesselbesitzerID = schluesselbesitzerID;
            data.Schluesselbesitzer = new SchluesselbesitzerAPI().Lade(schluesselbesitzerID);
            auswahlTypes = SchluesselzuteilungTypes.Schluessel;
            ((DelegateCommand)OpenAuswahlSchluesselCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)OpenAuswahlBesitzerCommand).RaiseCanExecuteChanged();
            ValidateBezeichnung(SchluesselBez, "SchluesselBez", "Schlüssel");
            this.RaisePropertyChanged("Schluesselbesitzer");
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        #region Bindings
        public String SchluesselBez
        {
            get
            {
                if (data.Schluessel == null)
                    return "";
                else
                    return data.Schluessel.Bezeichnung;
            }
        }
        public String Schluesselbesitzer
        {
            get
            {
                if (data.Schluesselbesitzer == null) 
                    return "";
                else 
                    return data.Schluesselbesitzer.Name; 
            }
        }

        public int? Anzahl
        {
            get => data.Anzahl;
            set
            {
                if (!string.Equals(data.Anzahl, value))
                {
                    ValidateAnzahl(value, "Anzahl");
                    data.Anzahl = value.GetValueOrDefault(0);
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public DateTime? ErhaltenAm
        {
            get => data.ErhaltenAm;
            set
            {

                if (!string.Equals(data.ErhaltenAm, value))
                {
                    ValidateEintrittsdatum(value);
                    data.ErhaltenAm = value.GetValueOrDefault();
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand OpenAuswahlSchluesselCommand { get; set; }
        public ICommand OpenAuswahlBesitzerCommand { get; set; }
        #endregion

        #region Commands
        protected override void ExecuteSaveCommand()
        {
            var API = new SchluesselverteilungAPI();
            try
            {
                API.TeileSchluesselZu(data);
                Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Schlüsselzuteilung gespeichert" }, StammdatenTypes.schluesselzuteilung);
           
            }
            catch (ZuWenigFreieSchluesselVorhandenException)
            {
                SendExceptionMessage("Es sind zu wenig Schlüssel frei");
                return;
            }
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.schluesselzuteilung);
        }
        private bool CanOpenAuswahlBesitzerCommand()
        {
            return auswahlTypes == SchluesselzuteilungTypes.Besitzer;
        }

        private bool CanExecuteOpenAuswahlSchluesselCommand()
        {
            return auswahlTypes == SchluesselzuteilungTypes.Schluessel;
        }

        private void ExecuteOpenAuswahlSchluesselCommand()
        {
            Messenger.Default.Send<OpenSchluesselAuswahlMessage>(new OpenSchluesselAuswahlMessage(OpenSchluesselAuswahlCallback), "SchluesselzuteilungStammdaten");
        }

        private void ExecuteOpenAuswahlBesitzerCommand()
        {
            Messenger.Default.Send<OpenSchluesselbesitzerAuswahlMessage>(new OpenSchluesselbesitzerAuswahlMessage(OpenSchluesselbesitzerAuswahlCallback), "SchluesselzuteilungStammdaten");
        }
        #endregion

        #region Callback
        private void OpenSchluesselbesitzerAuswahlCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                data.Schluesselbesitzer = new SchluesselbesitzerAPI().Lade(id);
                data.SchluesselbesitzerID = data.Schluesselbesitzer.ID;
                ValidateBezeichnung(Schluesselbesitzer, "Schluesselbesitzer", "Schlüssel");
                this.RaisePropertyChanged("Schluesselbesitzer");
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        private void OpenSchluesselAuswahlCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                data.Schluessel = new SchluesselAPI().Lade(id);
                data.SchluesselID = data.Schluessel.ID;
                ValidateBezeichnung(SchluesselBez, "SchluesselBez", "Schlüssel");
                this.RaisePropertyChanged("SchluesselBez");
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Validierung
        private bool ValidateBezeichnung(string bezeichnung, string fieldname, string messagefieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(bezeichnung, messagefieldname, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }

        private bool ValidateAnzahl(int? anzahl, string fieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateInteger(anzahl, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }
        private bool ValidateEintrittsdatum(DateTime? eintrittsdatum)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateDatum(eintrittsdatum, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "ErhaltenAm", validationErrors);

            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            data = new Schluesselzuteilung { };
            ErhaltenAm = DateTime.Now;
            Anzahl = null;
            state = State.Neu;
            auswahlTypes = SchluesselzuteilungTypes.Undefiniert;
        }
    }
}
