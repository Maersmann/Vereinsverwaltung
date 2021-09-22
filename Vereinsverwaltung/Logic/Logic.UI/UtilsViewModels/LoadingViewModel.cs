using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.UtilsViewModels
{
    public class LoadingViewModel : ViewModelBasis
    {
        private string beschreibung;
        public LoadingViewModel()
        {
            beschreibung = "";
        }

        public string Beschreibung
        {
            get => beschreibung;
            set
            {
                beschreibung = value;
                RaisePropertyChanged();
            }
        }
    }
}
