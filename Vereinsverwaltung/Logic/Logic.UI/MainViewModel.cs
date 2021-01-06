using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Vereinsverwaltung.Logic.Core;
using Vereinsverwaltung.Logic.UI.BaseViewModels;
using System;
using System.Windows.Input;

namespace Vereinsverwaltung.Logic.UI
{
    public class MainViewModel : ViewModelBasis
    {
        public MainViewModel()
        {
            Title = "Aktienübersicht";
            OpenConnectionCommand = new RelayCommand(() => ExecuteOpenConnectionCommand());
        }


        public ICommand OpenConnectionCommand { get; private set; }



        private void ExecuteOpenConnectionCommand()
        {
            var db = new DatabaseAPI();
            db.AktualisereDatenbank();
        }

    }
}