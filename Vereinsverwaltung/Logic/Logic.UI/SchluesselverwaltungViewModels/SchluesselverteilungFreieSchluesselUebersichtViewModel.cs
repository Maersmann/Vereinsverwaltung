using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using Logic.Core;
using Logic.UI.BaseViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungFreieSchluesselUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselModel, StammdatenTypes>
    {
        public SchluesselverteilungFreieSchluesselUebersichtViewModel()
        {
            MessageToken = "SchluesselverteilungFreieSchluesselUebersicht";
            Title = "Übersicht Freie Schlüssel";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung.ToString());
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel.ToString());
        }

        protected override int GetID() { return SelectedItem.ID; }
        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Schluessel; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/zuteilung/nichtverteilt"; }
        protected override bool WithPagination() { return true; }
    }
}
