using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Entitys.MitgliederEntitys;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Core.MitgliederCore;
using Vereinsverwaltung.Logic.Core.SchluesselCore;
using Vereinsverwaltung.Logic.Core.Validierungen.Base;
using Vereinsverwaltung.Logic.Messages.AuswahlMessages;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;
using Vereinsverwaltung.Logic.UI.InterfaceViewModels;

namespace Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselbesitzerStammdatenViewModel : ViewModelStammdaten<Schluesselbesitzer>, IViewModelStammdaten
    {
        public SchluesselbesitzerStammdatenViewModel()
        {
            messageToken = "SchluesselbesitzerStammdaten";
            Title = "Schlüsselbesitzer Stammdaten";  
            MitgliedHinterlegenCommand = new RelayCommand(() => ExcecuteMitgliedHinterlegenCommand());
            DeleteMitgliedDataCommand = new DelegateCommand(this.ExecuteDeleteMitgliedDataCommand, this.CanExecuteDeleteMitgliedDataCommand);
        }

        public void ZeigeStammdatenAn(int id)
        {
            var besitzer = new SchluesselbesitzerAPI().Lade(id);
            Name = besitzer.Name;
            data = besitzer;
            ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
            this.RaisePropertyChanged("KeinMitgliedHinterlegt");
            this.RaisePropertyChanged("Mitgliedsnr");
            state = State.Bearbeiten;
        }


        #region Bindings
        public String Name 
        { 
            get
            {
                return data.Name;
            }
            set
            {
                if(!string.Equals(data.Name,value))
                {
                    ValidateName(value);
                    data.Name = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Mitgliedsnr
        { 
            get
            {
                if (data.Mitglied == null)
                    return null;
                else
                    return data.Mitglied.Mitgliedsnr;
            }
        }
        public bool KeinMitgliedHinterlegt { get => !data.MitgliedID.HasValue; }
        public ICommand MitgliedHinterlegenCommand { get; set; }
        public ICommand DeleteMitgliedDataCommand { get; set; }
        #endregion

        #region Commands
        protected override void ExecuteSaveCommand()
        {
            var API = new SchluesselbesitzerAPI();
            try
            {
                if (state.Equals(State.Neu))
                {
                    API.Speichern(data);
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Schlüsselbesitzer gespeichert" }, StammdatenTypes.schluesselbesitzer);
                }
                else
                {
                    API.Aktualisieren(data);
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Schlüsselbesitzer aktualisiert" },StammdatenTypes.schluesselbesitzer);
                }
            }
            catch (Exception)
            {
                return;
            }
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.schluesselbesitzer);
        }

        private void ExcecuteMitgliedHinterlegenCommand()
        {
            Messenger.Default.Send<OpenMitgliedAuswahlMessage>(new OpenMitgliedAuswahlMessage(OpenMitgliedAuswahlCallback), messageToken);
        }

        private void OpenMitgliedAuswahlCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                data.MitgliedID = id;
                data.Mitglied = new MitgliedAPI().Lade(id);
                Name = data.Mitglied.Name;
                ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
                this.RaisePropertyChanged("KeinMitgliedHinterlegt");
                this.RaisePropertyChanged("Mitgliedsnr");
            }
        }

        private bool CanExecuteDeleteMitgliedDataCommand()
        {
            return !KeinMitgliedHinterlegt;
        }

        private void ExecuteDeleteMitgliedDataCommand()
        {
            data.MitgliedID = null;
            data.Mitglied = new Mitglied();
            ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
            this.RaisePropertyChanged("Mitgliedsnr");
            Name = "";
            this.RaisePropertyChanged("KeinMitgliedHinterlegt");
        }

        #endregion

        #region Validierung
        private bool ValidateName(string name)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(name, "" , out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Name", validationErrors);
            return isValid;
        }
        #endregion


        public override void Cleanup()
        {
            data = new Schluesselbesitzer { };
            Name = "";
            state = State.Neu;
        }
    }
}
