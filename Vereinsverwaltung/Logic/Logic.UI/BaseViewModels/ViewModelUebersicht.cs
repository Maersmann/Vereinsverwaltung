using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Messages.BaseMessages;

namespace Vereinsverwaltung.Logic.UI.BaseViewModels
{
    public class ViewModelUebersicht<T> : ViewModelBasis
    {
        protected ObservableCollection<T> itemList;

        protected T selectedItem;

        public ViewModelUebersicht()
        {
            itemList = new ObservableCollection<T>();
        }


        public void RegisterAktualisereViewMessage(ViewType inViewType)
        {
            Messenger.Default.Register<AktualisiereViewMessage>(this, inViewType, m => ReceiveAktualisiereViewMessage());
        }

        private void ReceiveAktualisiereViewMessage()
        {
            LoadData();
        }

        public virtual void LoadData() { }

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
    }
}
