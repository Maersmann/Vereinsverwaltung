using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Model.MitgliederEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Core;
using Vereinsverwaltung.Logic.Messages.MitgliederMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.MitgliederViewModels
{
    public class MitgliederUebersichtViewModel : ViewModelBasis
    {
        private ObservableCollection<Mitglied> mitglieder;

        private Mitglied selectedMitglied;

        public MitgliederUebersichtViewModel()
        {
            LoadData();
            NeuCommand = new RelayCommand(() => ExecuteNeuCommand());
            BearbeitenCommand = new DelegateCommand(this.ExecuteBearbeitenCommand, this.CanExecuteCommand);
            EntfernenCommand = new DelegateCommand(this.ExecuteEntfernenCommand, this.CanExecuteCommand);
            
        }

        #region Commands
        private void ExecuteNeuCommand()
        {
            Messenger.Default.Send<OpenMitgliederStammdatenMessage>(new OpenMitgliederStammdatenMessage {  State = State.Neu, MitgliedID = null  });
        }
        private void ExecuteBearbeitenCommand()
        {
            Messenger.Default.Send<OpenMitgliederStammdatenMessage>(new OpenMitgliederStammdatenMessage {  State = State.Bearbeiten, MitgliedID = selectedMitglied.ID });
        }

        public void LoadData()
        {
            mitglieder = new MitgliedAPI().LadeAlle();
            this.RaisePropertyChanged("Mitglieder");
        }

        private void ExecuteEntfernenCommand()
        {
            new MitgliedAPI().Entfernen(selectedMitglied.ID);
            mitglieder.Remove(selectedMitglied);
            this.RaisePropertyChanged("Mitglieder");

        }
        private bool CanExecuteCommand()
        {
            return selectedMitglied != null;
        }
        #endregion

        #region Bindings
        public Mitglied SelectedMitglied
        {
            get
            {
                return selectedMitglied;
            }
            set
            {
                selectedMitglied = value;
                this.RaisePropertyChanged();
                ((DelegateCommand)BearbeitenCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)EntfernenCommand).RaiseCanExecuteChanged();
            }
        }
        public IEnumerable<Mitglied> Mitglieder
        {
            get
            {
                return mitglieder;
            }
        }
        public ICommand NeuCommand { get; private set; }
        public ICommand BearbeitenCommand { get; private set; }
        public ICommand EntfernenCommand { get; private set; }

        #endregion
    }
}
