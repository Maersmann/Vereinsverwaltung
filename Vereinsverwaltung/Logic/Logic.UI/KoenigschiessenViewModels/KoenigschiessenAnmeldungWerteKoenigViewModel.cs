using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenAnmeldungWerteKoenigViewModel : ViewModelBasis, IKoenigschiessenAnmeldungWerteViewModel
    {
        public KoenigschiessenAnmeldungWerteKoenigViewModel()
        {
            Werte = new KoenigschiessenAnmeldungWerteKoenig();
        }

        public async void LadeWerte(int jahr)
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/KoenigschiessenAnmeldung/Werte/Koenig?jahr={jahr}");
                if (resp.IsSuccessStatusCode)
                {
                    Werte = await resp.Content.ReadAsAsync<KoenigschiessenAnmeldungWerteKoenig>();
                }

                RequestIsWorking = false;
            }
            OnPropertyChanged(nameof(Werte));
        }


        public KoenigschiessenAnmeldungWerteKoenig Werte { get; private set; }
    }
}
