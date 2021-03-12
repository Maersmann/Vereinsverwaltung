using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Data.Types.SchluesselverwaltungTypes;
using Vereinsverwaltung.Logic.Core.SchluesselCore;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselzuteilungHistoryUebersichtViewModel : ViewModelUebersicht<SchluesselzuteilungHistory>
    {
        private SchluesselzuteilungTypes auswahlTypes;
        private int id;
        public SchluesselzuteilungHistoryUebersichtViewModel()
        {
            MessageToken = "SchluesselzuteilungHistoryUebersicht";
            Title = "History Verteilung";
        }

        public void SetAuswahlState(int id, SchluesselzuteilungTypes auswahlTypes)
        {
            this.auswahlTypes = auswahlTypes;
            this.id = id;
            LoadData();
        }

        public override void LoadData()
        {
            if (auswahlTypes.Equals(SchluesselzuteilungTypes.Besitzer))
                itemList = new SchluesselzuteilungHistoryAPI().LadeAlleFuerBesitzer(id);
            else
                itemList = new SchluesselzuteilungHistoryAPI().LadeAlleFuerSchluessel(id);
            base.LoadData();
        }
    }
}
