﻿using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Logic.UI.BaseViewModels
{
    public class ViewModelLoadData<T> : ViewModelBasis
    {
        private StammdatenTypes typ;
        protected ICollectionView _customerView;
        protected ObservableCollection<T> itemList;
        protected T selectedItem;

        public ViewModelLoadData()
        {
            itemList = new ObservableCollection<T>();
            _customerView = CollectionViewSource.GetDefaultView(ItemList);
            _customerView.Filter = OnFilterTriggered;
        }
        public void RegisterAktualisereViewMessage(StammdatenTypes stammdaten)
        {
            typ = stammdaten;
            Messenger.Default.Register<AktualisiereViewMessage>(this, stammdaten, m => ReceiveAktualisiereViewMessage(m));
        }

        protected virtual void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            if (m.ID.HasValue)
                LoadData(m.ID.Value);
            else
                LoadData();
        }

        public virtual void LoadData() 
        {
            _customerView = (CollectionView)CollectionViewSource.GetDefaultView(itemList);
            _customerView.Filter = OnFilterTriggered;
            this.RaisePropertyChanged("ItemCollection");
            this.RaisePropertyChanged("ItemList"); 
        }
        public virtual void LoadData(int id) 
        {
            _customerView = (CollectionView)CollectionViewSource.GetDefaultView(ItemList);
            _customerView.Filter = OnFilterTriggered;
            this.RaisePropertyChanged("ItemCollection");
            this.RaisePropertyChanged("ItemList");
        }

        protected virtual bool OnFilterTriggered(object item)
        {
            return true;
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

        public override void Cleanup()
        {
            Messenger.Default.Unregister<AktualisiereViewMessage>(this, typ);
            base.Cleanup();
        }
    }
}
