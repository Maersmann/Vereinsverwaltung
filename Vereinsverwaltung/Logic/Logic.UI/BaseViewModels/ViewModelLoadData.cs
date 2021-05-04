using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logic.UI.BaseViewModels
{
    public class ViewModelLoadData : ViewModelBasis
    {
        private StammdatenTypes typ; 
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

        public virtual void LoadData() { this.RaisePropertyChanged("ItemList"); }
        public virtual void LoadData(int id) { this.RaisePropertyChanged("ItemList"); }


        public override void Cleanup()
        {
            Messenger.Default.Unregister<AktualisiereViewMessage>(this, typ);
            base.Cleanup();
        }
    }
}
