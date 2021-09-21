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
            return selectedItem == null ? null : (int?)selectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluessel; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/schluessel"; }

    }
}
