using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungFreieSchluesselUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselverteilungFreieSchluesselUebersichtModel>
    {
        public SchluesselverteilungFreieSchluesselUebersichtViewModel()
        {
            MessageToken = "SchluesselverteilungFreieSchluesselUebersicht";
            Title = "Übersicht Freie Schlüssel";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel);
        }

        protected override int GetID() { return selectedItem.ID; }
        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Schluessel; }
        public override void LoadData()
        {
            // Todo: Request
            /*
            itemList = new SchluesselAPI().LadeAlleFreien();
            this.RaisePropertyChanged("ItemList");
            */
        }
    }
}
