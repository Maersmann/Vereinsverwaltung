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
using Vereinsverwaltung.Logic.Core.MitgliederCore;
using Vereinsverwaltung.Logic.Messages.MitgliederMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.MitgliederViewModels
{
    public class MitgliederUebersichtViewModel : ViewModelUebersicht<Mitglied>
    {

        public MitgliederUebersichtViewModel()
        {
            Title = "Übersicht Mitglieder";
            RegisterAktualisereViewMessage(ViewType.viewMitgliederUebersicht);
            LoadData();
            NeuCommand = new RelayCommand(() => ExecuteNeuCommand());
            BearbeitenCommand = new DelegateCommand(this.ExecuteBearbeitenCommand, this.CanExecuteCommand);
            EntfernenCommand = new DelegateCommand(this.ExecuteEntfernenCommand, this.CanExecuteCommand);
            
        }

        public string MessageToken { set { messageToken = value; } }

        #region Commands
        private void ExecuteNeuCommand()
        {
            Messenger.Default.Send<OpenMitgliederStammdatenMessage>(new OpenMitgliederStammdatenMessage {  State = State.Neu, MitgliedID = null  });
        }
        private void ExecuteBearbeitenCommand()
        {
            Messenger.Default.Send<OpenMitgliederStammdatenMessage>(new OpenMitgliederStammdatenMessage {  State = State.Bearbeiten, MitgliedID = selectedItem.ID });
        }

        public override void LoadData()
        {
            itemList = new MitgliedAPI().LadeAlle();
            this.RaisePropertyChanged("ItemList");
        }

        private void ExecuteEntfernenCommand()
        {
            new MitgliedAPI().Entfernen(selectedItem.ID);
            itemList.Remove(selectedItem);
            this.RaisePropertyChanged("ItemList");

        }
        private bool CanExecuteCommand()
        {
            return selectedItem != null;
        }
        #endregion

        #region Bindings
        public override Mitglied SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                this.RaisePropertyChanged();
                ((DelegateCommand)BearbeitenCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)EntfernenCommand).RaiseCanExecuteChanged();
            }
        }
        public ICommand NeuCommand { get; private set; }
        public ICommand BearbeitenCommand { get; private set; }
        public ICommand EntfernenCommand { get; private set; }

        #endregion
    }
}
