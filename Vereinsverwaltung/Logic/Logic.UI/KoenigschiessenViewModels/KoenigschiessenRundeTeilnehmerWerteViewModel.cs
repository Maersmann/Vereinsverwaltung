using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenRundeTeilnehmerWerteViewModel : ViewModelBasis
    {
        public KoenigschiessenRundeTeilnehmerWerteViewModel()
        {
            Werte = new KoenigschiesenRundeTeilnehmerWerteModel();
        }

        public async void LadeWerte(int jahr, KoenigschiessenArt art, int runde)
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/KoenigschiessenRunde/Werte?jahr={jahr}&runde={runde}&art={art}");
                if (resp.IsSuccessStatusCode)
                {
                    Werte = await resp.Content.ReadAsAsync<KoenigschiesenRundeTeilnehmerWerteModel>();
                }

                RequestIsWorking = false;
            }
            RaisePropertyChanged(nameof(Werte));
        }


        public KoenigschiesenRundeTeilnehmerWerteModel Werte { get; private set; }
    }
}
