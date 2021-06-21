using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungFreieSchluesselUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselModel>
    {
        public SchluesselverteilungFreieSchluesselUebersichtViewModel()
        {
            MessageToken = "SchluesselverteilungFreieSchluesselUebersicht";
            Title = "Übersicht Freie Schlüssel";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel);
        }

        protected override int GetID() { return selectedItem.ID; }
        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Schluessel; }
        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/nichtverteilt");
                if (resp2.IsSuccessStatusCode)
                    itemList = await resp2.Content.ReadAsAsync<ObservableCollection<SchluesselModel>>();
            }
            base.LoadData();
        }
    }
}
