using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Data.Types.SchluesselverwaltungTypes;
using Vereinsverwaltung.Logic.Core.SchluesselCore;
using Vereinsverwaltung.Logic.Core.SchluesselCore.Exceptions;
using Vereinsverwaltung.Logic.Messages.SchluesselMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<Schluessel>
    {
        public SchluesselUebersichtViewModel()
        {
            MessageToken = "SchluesselUebersicht";
            Title = "Übersicht Schlüssel";
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel);
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
            OpenHistorieCommand = new DelegateCommand(this.ExecuteOpenHistorieCommand, this.CanExecuteCommand);
        }

        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluessel; }
        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Schluessel; }

        public override void LoadData()
        {
            itemList = new SchluesselAPI().LadeAlle();
            this.RaisePropertyChanged("ItemList");
        }


        #region Bindings
        public ICommand OpenHistorieCommand { get; set; }

        public override Schluessel SelectedItem 
        { 
            get => base.SelectedItem;
            set
            {
                ((DelegateCommand)OpenHistorieCommand).RaiseCanExecuteChanged();
                base.SelectedItem = value;
            }
        }
        #endregion

        #region Commands
        protected override void ExecuteEntfernenCommand()
        {
            try
            {
                new SchluesselAPI().Entfernen(selectedItem.ID);
            }
            catch (SchluesselIstZugeteiltException)
            {
                SendExceptionMessage("Schlüssel kann nicht gelöscht werden" + Environment.NewLine + Environment.NewLine + "Schlüssel ist Besitzer zugeordnet");
                return;
            }
            
            SendInformationMessage("Schlüssel gelöscht");
            base.ExecuteEntfernenCommand();
        }

        private void ExecuteOpenHistorieCommand()
        {
            Messenger.Default.Send<OpenSchluesselzuteilungHistoryUebersichtMessage>(new OpenSchluesselzuteilungHistoryUebersichtMessage { AuswahlTypes = SchluesselzuteilungTypes.Schluessel, ID = selectedItem.ID}, messageToken);
        }

        #endregion
    }
}
