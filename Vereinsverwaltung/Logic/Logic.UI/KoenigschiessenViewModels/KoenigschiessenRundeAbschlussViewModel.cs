using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels.DTOs;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenRundeAbschlussViewModel : ViewModelBasis
    {
        private KoenigschiessenAbschlussDTO koenigschiessenAbschluss;
        public KoenigschiessenRundeAbschlussViewModel()
        {
            koenigschiessenAbschluss = new KoenigschiessenAbschlussDTO();
            Title = "Runde beendet";
            NaechsteRundeCommand = new RelayCommand(() => ExecuteNaechsteRundeCommand());
            Bestaetigt = false;
        }

        public void ZeigeDatenAn(KoenigschiessenAbschlussDTO koenigschiessenAbschluss)
        {
            this.koenigschiessenAbschluss = koenigschiessenAbschluss;
            RaisePropertyChanged(nameof(KoenigschiessenAbschluss));
        }

        public bool Bestaetigt { get; private set; }


        #region Bindings
        public ICommand NaechsteRundeCommand { get; set; }

        public KoenigschiessenAbschlussDTO KoenigschiessenAbschluss => koenigschiessenAbschluss;
        #endregion


        #region Commands

        private void ExecuteNaechsteRundeCommand()
        {
            Messenger.Default.Send(new CloseViewMessage(), "KoenigschiessenRundeAbschluss");
        }
        #endregion

    }
}
