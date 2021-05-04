using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.SchluesselMessages;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungBesitzerUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselverteilungBesitzerUebersichtModel>
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
            // Todo: Request
            /*
            itemList = new SchluesselverteilungAPI().LadeVerteilungSchluesselbesitzer();
            this.RaisePropertyChanged("ItemList");
            */
        }

        #region Bindings

        public override SchluesselverteilungBesitzerUebersichtModel SelectedItem 
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
