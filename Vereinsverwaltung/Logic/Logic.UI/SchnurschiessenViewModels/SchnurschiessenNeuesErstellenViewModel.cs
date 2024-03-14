using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Data.Model.SchnurrschiessenModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenNeuesErstellenViewModel : ViewModelStammdaten<NeuesSchnurschiessenModel, StammdatenTypes>, IViewModelStammdaten
    {
        private bool neuesSchnurschiessenErstellt;
        public SchnurschiessenNeuesErstellenViewModel()
        {
            Title = "Neues Schnurschiessen";
            neuesSchnurschiessenErstellt = false;
        }

        public void ZeigeStammdatenAnAsync(int id)
        {
        }
        public bool NeuesSchnurschiessenErstellt => neuesSchnurschiessenErstellt;
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnurschiessen;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                Data.Erstelldatum = DateTime.Now;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/schnurschiessen", Data);
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    neuesSchnurschiessenErstellt = true;
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Neues Schnurschiessen erstellt" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Neues Schnurschiessen konnte nicht erstellt werden.");
                }
            }
        }
        #endregion

        public override void Cleanup()
        {
            Data = new NeuesSchnurschiessenModel();
            state = State.Neu;
        }
    }
}
