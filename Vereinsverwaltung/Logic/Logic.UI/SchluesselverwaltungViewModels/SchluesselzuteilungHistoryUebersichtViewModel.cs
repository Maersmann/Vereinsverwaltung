using Data.Model.SchluesselverwaltungModels;
using Data.Types.SchluesselverwaltungTypes;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselzuteilungHistoryUebersichtViewModel : ViewModelUebersicht<SchluesselzuteilungHistoryUebersichtModel>
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
            // Todo: Request
            /*
            if (auswahlTypes.Equals(SchluesselzuteilungTypes.Besitzer))
                itemList = new SchluesselzuteilungHistoryAPI().LadeAlleFuerBesitzer(id);
            else
                itemList = new SchluesselzuteilungHistoryAPI().LadeAlleFuerSchluessel(id);
            base.LoadData();
            */
        }
    }
}
