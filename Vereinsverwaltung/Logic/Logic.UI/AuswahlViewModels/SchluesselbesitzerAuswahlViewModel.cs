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
    public class SchluesselbesitzerAuswahlViewModel : ViewModelAuswahl<SchluesselbesitzerAuswahlModel>
    {
        public SchluesselbesitzerAuswahlViewModel()
        {
            Title = "Auswahl Besitzer";
            LoadData();
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselbesitzer);
        }

        public int? SchluesselbestizerID()
        {
            if (selectedItem == null) return null;
            else return selectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluesselbesitzer; }

        public override void LoadData()
        {
            // Todo: Request
            //itemList = new SchluesselbesitzerAPI().LadeAlle();
            base.LoadData();
        }
    }
}
