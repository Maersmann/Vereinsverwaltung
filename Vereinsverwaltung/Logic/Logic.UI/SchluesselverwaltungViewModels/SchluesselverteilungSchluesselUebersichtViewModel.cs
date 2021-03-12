using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Data.Types.SchluesselverwaltungTypes;
using Vereinsverwaltung.Logic.Core.SchluesselCore;
using Vereinsverwaltung.Logic.Core.SchluesselCore.Models;
using Vereinsverwaltung.Logic.Messages.SchluesselMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungSchluesselUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselzuteilungSchluesselModel>
    {
        public SchluesselverteilungSchluesselUebersichtViewModel()
        {
            MessageToken = "SchluesselverteilungSchluesselUebersicht";
            Title = "Übersicht Verteilung Schlüssel";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
        }

        protected override int GetID() { return selectedItem.SchluesselID; }
        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Schluessel; }

        public override void LoadData()
        {
            itemList = new SchluesselverteilungAPI().LadeVerteilungSchluessel();
            this.RaisePropertyChanged("ItemList");
        }

        #region Bindings

        public override SchluesselzuteilungSchluesselModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                if (selectedItem != null)
                {
                    Messenger.Default.Send<LoadSchluesselverteilungSchluesselDetailMessage>(new LoadSchluesselverteilungSchluesselDetailMessage { ID = selectedItem.SchluesselID }, messageToken);
                }
            }
        }
        #endregion
    }
}
