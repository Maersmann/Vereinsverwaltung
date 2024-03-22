using Base.Logic.ViewModels;

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
                OnPropertyChanged();
            }
        }
    }
}
