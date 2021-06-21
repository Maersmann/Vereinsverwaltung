using GalaSoft.MvvmLight.Messaging;
using Logic.Core.OptionenLogic;
using Logic.Messages.BaseMessages;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Logic.UI.KonfigruationViewModels
{
    public class KonfigruationViewModel : ViewModelBasis
    {
        public KonfigruationViewModel()
        {
            Title = "Konfiguration der Software";
        }

        protected override void ExecuteCloseWindowCommand(Window window)
        {
            base.ExecuteCloseWindowCommand(window);
            if (!new BackendLogic().IstINIVorhanden())
                Messenger.Default.Send<CloseApplicationMessage>(new CloseApplicationMessage());
        }


    }
}
