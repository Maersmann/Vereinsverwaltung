using Data.Model.AuswahlModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.UI.BaseViewModels;
using System.Net.Http;
using Logic.Core;
using System.Collections.ObjectModel;

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
            return selectedItem == null ? null : (int?)selectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluesselbesitzer; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/besitzer"; }

    }
}
