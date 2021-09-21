using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.SchluesselMessages;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungSchluesselUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselverteilungSchluesselUebersichtModel>
    {
        public SchluesselverteilungSchluesselUebersichtViewModel()
        {
            MessageToken = "SchluesselverteilungSchluesselUebersicht";
            Title = "Übersicht Verteilung Schlüssel";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel);
        }

        protected override int GetID() { return selectedItem.SchluesselID; }
        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Schluessel; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/zuteilung/schluessel"; }

        #region Bindings

        public override SchluesselverteilungSchluesselUebersichtModel SelectedItem
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
