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
    public class ViewModelAuswahl<T> : ViewModelLoadData<T>
    {
        
        public ViewModelAuswahl()
        {
            
            NewItemCommand = new RelayCommand(this.ExcecuteNewItemCommand);
            
        }

       

        protected virtual void ExcecuteNewItemCommand()
        {
            Messenger.Default.Send<BaseStammdatenMessage>(new BaseStammdatenMessage { State = State.Neu, ID = null, Stammdaten = GetStammdatenType() });
        }

       

        public ICommand NewItemCommand { get; set; }

        protected virtual StammdatenTypes GetStammdatenType() { return 0; }
    }
}
