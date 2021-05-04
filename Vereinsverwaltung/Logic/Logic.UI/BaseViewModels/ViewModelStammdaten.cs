﻿using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.BaseViewModels
{
    public class ViewModelStammdaten<T> : ViewModelValidate
    {
        protected T data;
        protected State state;
        protected bool LoadAktie;
        public ICommand SaveCommand { get; protected set; }

        public ViewModelStammdaten()
        {
            LoadAktie = false;
            SaveCommand = new DelegateCommand(this.ExecuteSaveCommand, this.CanExecuteSaveCommand);
            Cleanup();
        }


        protected virtual StammdatenTypes GetStammdatenTyp() { return 0; }

        protected virtual bool CanExecuteSaveCommand()
        {
            return ValidationErrors.Count == 0;
        }

        protected virtual void ExecuteSaveCommand()
        {
            /*
            if (state.Equals(State.Neu))
            {
                api.Speichern(data);
                Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
            }
            else
            {
                api.Aktualisieren(data);
                Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Aktualisiert" }, GetStammdatenTyp());
            }
            */
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), GetStammdatenTyp());
        }
    }
}
