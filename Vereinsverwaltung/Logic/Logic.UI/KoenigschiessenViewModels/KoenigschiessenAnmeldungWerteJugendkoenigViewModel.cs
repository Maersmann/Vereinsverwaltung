using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenAnmeldungWerteJugendkoenigViewModel : ViewModelBasis, IKoenigschiessenAnmeldungWerteViewModel
    {
        public KoenigschiessenAnmeldungWerteJugendkoenigViewModel()
        {
            Werte = new KoenigschiessenAnmeldungWerteJugendkoenig();
        }

        public async void LadeWerte(int jahr)
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/KoenigschiessenAnmeldung/Werte/Jugendkoenig?jahr={jahr}");
                if (resp.IsSuccessStatusCode)
                {
                    Werte = await resp.Content.ReadAsAsync<KoenigschiessenAnmeldungWerteJugendkoenig>();
                }

                RequestIsWorking = false;
            }
            RaisePropertyChanged(nameof(Werte));
        }


        public KoenigschiessenAnmeldungWerteJugendkoenig Werte { get; private set; }
    }
}
