using Data.Types;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.BaseViewModels
{
    public class ViewModelUebersicht<T> : ViewModelLoadData<T>
    {
       
        public ViewModelUebersicht()
        {
            EntfernenCommand = new DelegateCommand(this.ExecuteEntfernenCommand, this.CanExecuteCommand);
            NeuCommand = new RelayCommand(() => ExecuteNeuCommand());
            BearbeitenCommand = new DelegateCommand(this.ExecuteBearbeitenCommand, this.CanExecuteCommand);
            LoadData();
        }    

        protected virtual int GetID() { return 0; }
        protected virtual StammdatenTypes GetStammdatenType() { return 0; }


        public override T SelectedItem
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

        protected bool CanExecuteCommand()
        {
            return selectedItem != null;
        }

        public ICommand NeuCommand { get; protected set; }
        public ICommand BearbeitenCommand { get; protected set; }
        public ICommand EntfernenCommand { get; protected set; }

        protected virtual void ExecuteEntfernenCommand()
        {
            itemList.Remove(selectedItem);
            this.RaisePropertyChanged("ItemList");
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), GetStammdatenType());
        }
        protected virtual void ExecuteBearbeitenCommand()
        {
            Messenger.Default.Send<BaseStammdatenMessage>(new BaseStammdatenMessage { State = State.Bearbeiten, ID = GetID(), Stammdaten = GetStammdatenType() });
        }
        protected virtual void ExecuteNeuCommand()
        {
            Messenger.Default.Send<BaseStammdatenMessage>(new BaseStammdatenMessage { State = State.Neu, ID = null, Stammdaten = GetStammdatenType() });
        }
    }
}
