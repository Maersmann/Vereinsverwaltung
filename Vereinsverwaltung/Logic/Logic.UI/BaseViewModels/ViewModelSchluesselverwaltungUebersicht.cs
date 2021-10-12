using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using Data.Types.SchluesselverwaltungTypes;
using Logic.Messages.SchluesselMessages;
using Base.Logic.ViewModels;

namespace Logic.UI.BaseViewModels
{
    public class ViewModelSchluesselverwaltungUebersicht<T1, T2> : ViewModelUebersicht<T1, T2>
    {
        public ViewModelSchluesselverwaltungUebersicht()
        {
            OpenZuteilungCommand = new RelayCommand(() => ExecuteOpenZuteilungCommand());
            OpenRueckgabeCommand = new RelayCommand(() => ExecuteOpenRueckgabeCommand());
        }
        protected virtual SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return 0; }

        public ICommand OpenZuteilungCommand { get; private set; }
        public ICommand OpenRueckgabeCommand { get; private set; }

        private void ExecuteOpenZuteilungCommand()
        {
            Messenger.Default.Send(new OpenSchluesselzuteilungMessage { ID = GetID() }, messageToken);
        }

        private void ExecuteOpenRueckgabeCommand()
        {
            Messenger.Default.Send(new OpenSchluesselRueckgabeMessage { ID = GetID(), AuswahlTypes = GetSchluesselzuteilungAuswahlTyp() }, messageToken);
        }
    }
}
