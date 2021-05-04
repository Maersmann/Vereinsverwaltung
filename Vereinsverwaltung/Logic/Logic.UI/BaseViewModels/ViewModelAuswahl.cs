using Data.Types;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Logic.UI.BaseViewModels
{
    public class ViewModelAuswahl<T> : ViewModelLoadData
    {
        protected ICollectionView _customerView;
        public ViewModelAuswahl()
        {
            itemList = new ObservableCollection<T>();
            NewItemCommand = new RelayCommand(this.ExcecuteNewItemCommand);
            _customerView = CollectionViewSource.GetDefaultView(ItemList);
            _customerView.Filter = OnFilterTriggered;
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
        public ICollectionView ItemCollection { get { return _customerView; } }

       

        protected virtual void ExcecuteNewItemCommand()
        {
            Messenger.Default.Send<BaseStammdatenMessage>(new BaseStammdatenMessage { State = State.Neu, ID = null, Stammdaten = GetStammdatenType() });
        }

        protected virtual bool OnFilterTriggered(object item)
        {
            return true;
        }

        public override void LoadData()
        {
            _customerView = (CollectionView)CollectionViewSource.GetDefaultView(ItemList);
            _customerView.Filter = OnFilterTriggered;
            base.LoadData();
        }

        public ICommand NewItemCommand { get; set; }

        protected ObservableCollection<T> itemList;
        protected T selectedItem;
        protected virtual StammdatenTypes GetStammdatenType() { return 0; }
    }
}
