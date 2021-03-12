using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
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
using Vereinsverwaltung.Logic.Messages.SchluesselMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungFreieSchluesselUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<Schluessel>
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
            itemList = new SchluesselAPI().LadeAlleFreien();
            this.RaisePropertyChanged("ItemList");
        }
    }
}
