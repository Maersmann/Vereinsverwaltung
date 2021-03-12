using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Messages.BaseMessages;

namespace Vereinsverwaltung.Logic.UI.BaseViewModels
{
    public class ViewModelAuswahl<T> : ViewModelLoadData
    {
        public ViewModelAuswahl()
        {
            itemList = new ObservableCollection<T>();
            NewItemCommand = new RelayCommand(this.ExcecuteNewItemCommand);
        }

        public virtual T SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                this.RaisePropertyChanged();
            }
        }
        public IEnumerable<T> ItemList
        {
            get
            {
                return itemList;
            }
        }

        protected virtual void ExcecuteNewItemCommand()
        {
            Messenger.Default.Send<BaseStammdatenMessage>(new BaseStammdatenMessage { State = State.Neu, ID = null, Stammdaten = GetStammdatenType() });
        }

        public ICommand NewItemCommand { get; set; }

        protected ObservableCollection<T> itemList;
        protected T selectedItem;
        protected virtual StammdatenTypes GetStammdatenType() { return 0; }
    }
}
