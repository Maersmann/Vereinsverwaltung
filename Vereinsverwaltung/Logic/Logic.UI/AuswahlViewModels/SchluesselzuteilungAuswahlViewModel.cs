using Data.Model.AuswahlModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Logic.UI.AuswahlViewModels
{
    public class SchluesselzuteilungAuswahlViewModel : ViewModelAuswahl<SchluesselzuteilungAuswahlModel>
    {
        private SchluesselzuteilungTypes auswahlTypes;
        private int id;

        public SchluesselzuteilungAuswahlViewModel()
        {
            Title = "Auswahl Besitzer";
            LoadData();
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
        }

        public void SetAuswahlState(int id, SchluesselzuteilungTypes auswahlTypes)
        {
            this.auswahlTypes = auswahlTypes;
            this.id = id;
            LoadData();
        }

        public int? SchluesselzuteilungID()
        {
            if (selectedItem == null) return null;
            else return selectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluesselzuteilung; }

        public override void LoadData()
        {
            // Todo: Request
            /*
            if (auswahlTypes.Equals(SchluesselzuteilungTypes.Besitzer))
                itemList = new SchluesselzuteilungAPI().LadeAlleFuerBesitzer(id);
            else
                itemList = new SchluesselzuteilungAPI().LadeAlleFuerSchluessel(id);
            base.LoadData();
            */
        }

    }
}
