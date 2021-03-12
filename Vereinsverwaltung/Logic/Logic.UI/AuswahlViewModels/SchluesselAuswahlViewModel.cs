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
    public class SchluesselAuswahlViewModel : ViewModelAuswahl<Schluessel>
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
            itemList = new SchluesselAPI().LadeAlle();
            base.LoadData();
        }
    }
}
