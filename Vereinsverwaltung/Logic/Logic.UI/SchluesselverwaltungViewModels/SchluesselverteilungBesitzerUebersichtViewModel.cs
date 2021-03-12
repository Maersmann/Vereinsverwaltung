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
    public class SchluesselverteilungBesitzerUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselzuteilungBesitzerModel>
    {
        public SchluesselverteilungBesitzerUebersichtViewModel()
        {
            MessageToken = "SchluesselverteilungBesitzerUebersicht";
            Title = "Übersicht Verteilung Schlüsselbesitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
        }

        protected override int GetID() { return selectedItem.SchluesselbesitzerID; }

        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Besitzer; }
        public override void LoadData()
        {
            itemList = new SchluesselverteilungAPI().LadeVerteilungSchluesselbesitzer();
            this.RaisePropertyChanged("ItemList");
        }

        #region Bindings

        public override SchluesselzuteilungBesitzerModel SelectedItem 
        { 
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                if (selectedItem != null)
                {
                    Messenger.Default.Send<LoadSchluesselverteilungBesitzerDetailMessage>(new LoadSchluesselverteilungBesitzerDetailMessage { ID = selectedItem.SchluesselbesitzerID }, messageToken);
                }
            } 
        }
        #endregion
    }
}
