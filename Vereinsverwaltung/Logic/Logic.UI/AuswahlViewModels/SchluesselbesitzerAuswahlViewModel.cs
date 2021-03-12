using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Core.SchluesselCore;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.AuswahlViewModels
{
    public class SchluesselbesitzerAuswahlViewModel : ViewModelAuswahl<Schluesselbesitzer>
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
            itemList = new SchluesselbesitzerAPI().LadeAlle();
            base.LoadData();
        }
    }
}
