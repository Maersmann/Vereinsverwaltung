using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Data.Types.SchluesselverwaltungTypes;
using Vereinsverwaltung.Logic.Core.SchluesselCore;
using Vereinsverwaltung.Logic.Core.SchluesselCore.Exceptions;
using Vereinsverwaltung.Logic.Core.SchluesselCore.Models;
using Vereinsverwaltung.Logic.Core.Validierungen.Base;
using Vereinsverwaltung.Logic.Messages.AuswahlMessages;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselRueckgabeStammdatenViewModel : ViewModelStammdaten<SchluesselRueckgabeStammdatenModel>
    {
        private SchluesselzuteilungTypes typ;
        private int id;
        private bool schluesselAusgewaehlt;

        public SchluesselRueckgabeStammdatenViewModel()
        {
            Cleanup();
            Title = "Schlüsselrückgabe";
            OpenAuswahlSchluesselzuteilungCommand = new RelayCommand(() => ExecuteOpenAuswahlSchluesselzuteilungCommand());
        }

        public void SetInformation(int id, SchluesselzuteilungTypes typ)
        {
            RueckgabeAm = DateTime.Now;
            this.typ = typ;
            this.id = id;
        }

        #region Bindings
        public String SchluesselBez => data.Schluesselbezeichnung;

        public String Schluesselbesitzer => data.SchluesselbesitzerName;

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
        public DateTime? RueckgabeAm
        {
            get => data.RueckgabeAm;
            set
            {

                if (!string.Equals(data.RueckgabeAm, value))
                {
                    ValidateDatum(value);
                    data.RueckgabeAm = value.GetValueOrDefault();
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand OpenAuswahlSchluesselzuteilungCommand { get; set; }
        #endregion

        #region Commands
        protected override void ExecuteSaveCommand()
        {
            var API = new SchluesselverteilungAPI();
            try
            {
                API.ErhalteSchluesselZurueck(data.SchluesselzuteilungID, data.Anzahl, data.RueckgabeAm);
                Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Rückgabe gespeichert" }, StammdatenTypes.schluesselrueckgabe);

            }
            catch (ZuVieleSchlüsselZurueckgegeben)
            {
                SendExceptionMessage("Es werden zu viele Schlüssel zurückgegeben");
                return;
            }
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.schluesselzuteilung);
        }


        private void ExecuteOpenAuswahlSchluesselzuteilungCommand()
        {
            Messenger.Default.Send<OpenSchluesselzuteilungAuswahlMessage>(new OpenSchluesselzuteilungAuswahlMessage(OpenSchluesselzuteilungAuswahlCallback, id, typ), "SchluesselRueckgabeStammdaten");
        }

        protected override bool CanExecuteSaveCommand()
        {
            return base.CanExecuteSaveCommand() && schluesselAusgewaehlt;
        }
        #endregion

        #region Callback
        private void OpenSchluesselzuteilungAuswahlCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                var zuteilung = new SchluesselzuteilungAPI().Lade(id);
                data.SchluesselzuteilungID = id;
                data.Schluesselbezeichnung = zuteilung.Schluessel.Bezeichnung;
                data.SchluesselbesitzerName = zuteilung.Schluesselbesitzer.Name;
                Anzahl = zuteilung.Anzahl;
                this.RaisePropertyChanged("SchluesselBez");
                this.RaisePropertyChanged("Schluesselbesitzer");
                schluesselAusgewaehlt = true;
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
        private bool ValidateDatum(DateTime? eintrittsdatum)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateDatum(eintrittsdatum, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "RueckgabeAm", validationErrors);

            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            data = new SchluesselRueckgabeStammdatenModel { };
            RueckgabeAm = DateTime.Now;
            Anzahl = null;
            state = State.Neu;
            schluesselAusgewaehlt = false;
        }
    }
}
