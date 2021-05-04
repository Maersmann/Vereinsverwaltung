using Data.Model.AuswahlModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.UI.BaseViewModels;

namespace Logic.UI.AuswahlViewModels
{
    public class SchluesselAuswahlViewModel : ViewModelAuswahl<SchluesselAuswahlModels>
    {
        public SchluesselAuswahlViewModel()
        {
            Title = "Auswahl Schlüssel";
            LoadData();
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel);
        }

        public int? SchluesselID()
        {
            if (selectedItem == null) return null;
            else return selectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluessel; }

        public override void LoadData()
        {
//Todo: Request
            //itemList = new SchluesselAPI().LadeAlle();
            //base.LoadData();
        }
    }
}
