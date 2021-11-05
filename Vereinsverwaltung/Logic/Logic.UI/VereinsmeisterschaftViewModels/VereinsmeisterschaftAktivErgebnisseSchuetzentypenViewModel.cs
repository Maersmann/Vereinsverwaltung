using Base.Logic.Core;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftAktivErgebnisseSchuetzentypenViewModel : ViewModelUebersicht<VereinsmeisterschaftSchuetzeErgebnisseJeBereichModel, StammdatenTypes>
    {
        private VereinsmeisterschaftMitInfoModel vereinsmeisterschaft;
        public VereinsmeisterschaftAktivErgebnisseSchuetzentypenViewModel()
        {
            MessageToken = "VereinsmeisterschaftAktivErgebnisseSchuetzen";
            Title = "Übersicht Bereiche Aktive Vereinsmeisterschaft";
            _ = LoadVereinsmeisterschaft();
        }

        protected override string GetREST_API() { return $"/api/VereinsmeisterschaftschuetzeErgebnisse/Vereinsmeisterschaft/Bereiche?vereinsmeisterschaftId={(vereinsmeisterschaft != null ? vereinsmeisterschaft.ID : 0)}"; }
        protected override bool WithPagination() { return true; }
        protected override bool LoadingOnCreate() => false;

        private async Task LoadVereinsmeisterschaft()
        {
            vereinsmeisterschaft = new VereinsmeisterschaftMitInfoModel();
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                string URL = GlobalVariables.BackendServer_URL + "/api/vereinsmeisterschaften/aktiv";
                HttpResponseMessage resp = await Client.GetAsync(URL);
                if (resp.IsSuccessStatusCode)
                {
                    Response<VereinsmeisterschaftMitInfoModel> VereinsmeisterschaftResponse = await resp.Content.ReadAsAsync<Response<VereinsmeisterschaftMitInfoModel>>();
                    vereinsmeisterschaft = VereinsmeisterschaftResponse.Data;
                    await LoadData();
                    RequestIsWorking = false;
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    RequestIsWorking = false;
                    SendInformationMessage("Keine Vereinsmeisterschaft Aktiv");
                }
            }
        }

        #region Bindings
        public override VereinsmeisterschaftSchuetzeErgebnisseJeBereichModel SelectedItem 
        { 
            get => base.SelectedItem; 
            set 
            {
                base.SelectedItem = value;
                if (SelectedItem != null)
                {
                    Messenger.Default.Send(new LoadVereinsmeisterschaftAktivErgebnisseVonSchuetzentypMessage { SchuetzeTyp = SelectedItem.VereinsmeisterschaftSchuetzeTyp, VereinsmeisterschaftID = vereinsmeisterschaft.ID }, messageToken);
                }
            }
        }
        #endregion
    }
}
