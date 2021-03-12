using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Types.SchluesselverwaltungTypes;
using Vereinsverwaltung.Logic.Messages.SchluesselMessages;

namespace Vereinsverwaltung.Logic.UI.BaseViewModels
{
    public class ViewModelSchluesselverwaltungUebersicht<T> : ViewModelUebersicht<T>
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
            Messenger.Default.Send<OpenSchluesselzuteilungMessage>(new OpenSchluesselzuteilungMessage { ID = GetID() }, messageToken);
        }

        private void ExecuteOpenRueckgabeCommand()
        {
            Messenger.Default.Send<OpenSchluesselRueckgabeMessage>(new OpenSchluesselRueckgabeMessage { ID = GetID(), AuswahlTypes = GetSchluesselzuteilungAuswahlTyp() }, messageToken);
        }
    }
}
