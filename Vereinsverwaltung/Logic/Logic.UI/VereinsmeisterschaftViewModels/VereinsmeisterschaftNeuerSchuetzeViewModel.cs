using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.AuswahlTypes;

using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.BaseMessages;
using Logic.Messages.VereinsmeisterschaftMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System.Net.Http;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftNeuerSchuetzeViewModel : ViewModelStammdaten<VereinsmeisterschaftschuetzeErgebnisModel, StammdatenTypes>, IViewModelStammdaten
    {
        public VereinsmeisterschaftNeuerSchuetzeViewModel()
        {
            messageToken = "VereinsmeisterschaftNeuerSchuetze";
            Title = "Neuer Schütze";
            SchuetzeHinterlegenCommand = new RelayCommand(() => ExcecuteSchuetzeHinterlegenCommand());
            GruppeHinterlegenCommand = new RelayCommand(() => ExcecuteGruppeHinterlegenCommand());
        }
        public int VereinsmeisterschaftID { set => Data.VereinsmeisterschaftID = value; }

        public void ZeigeStammdatenAnAsync(int id)
        {

        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.vereinsmeisterschaftSchuetzeErgebnis;

        #region Bindings
        public string SchuetzeName
        {
            get => Data.SchuetzenName;
            set
            {
                if (RequestIsWorking || !Equals(Data.SchuetzenName, value))
                {
                    Data.SchuetzenName = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string GruppenName
        {
            get => Data.Gruppenname;
            set
            {
                if (RequestIsWorking || !Equals(Data.Gruppenname, value))
                {
                    Data.Gruppenname = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SchuetzeHinterlegenCommand { get; set; }
        public ICommand GruppeHinterlegenCommand { get; set; }
        #endregion

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/VereinsmeisterschaftschuetzeErgebnisse", Data);

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                }
                else
                {
                    SendExceptionMessage("Schütze konnte nicht gespeichert werden."+ await resp.Content.ReadAsStringAsync());
                }
                RequestIsWorking = false;

            }
        }

        private void ExcecuteSchuetzeHinterlegenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenSchuetzeAuswahlMessage(OpenSchuetzeHinterlegenCommandCallback, AuswahlVereinsmeisterschaftSchuetzeTypes.nurFreieFuerVereinsmeisterschaft) { VereinsmeisterschaftID = Data.VereinsmeisterschaftID, Geschlecht = Data.Geschlecht }, messageToken) ;
        }

        private void ExcecuteGruppeHinterlegenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenVereinsmeisterschaftFreieGruppeAuswahlMessage(OpenGruppeHinterlegenCommandCallback, Data.VereinsmeisterschaftID, Data.Geschlecht), messageToken);
        }

        private async void OpenSchuetzeHinterlegenCommandCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    RequestIsWorking = true;

                    HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Schuetzen/{id}");

                    if (resp.IsSuccessStatusCode)
                    {
                        Response<SchuetzeModel> SchuetzeResp = await resp.Content.ReadAsAsync<Response<SchuetzeModel>>();
                        Data.SchuetzeID = id;
                        SchuetzeName = SchuetzeResp.Data.Name;
                        if(!Data.Geschlecht.HasValue)
                        {
                            Data.Geschlecht = SchuetzeResp.Data.Geschlecht;
                        }
                    }
                    RequestIsWorking = false;
                }
            }
        }

        private async void OpenGruppeHinterlegenCommandCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    RequestIsWorking = true;

                    HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Schiessgruppen/{id}");

                    if (resp.IsSuccessStatusCode)
                    {
                        Response<SchiessgruppeModel> GruppeResp = await resp.Content.ReadAsAsync<Response<SchiessgruppeModel>>();
                        Data.SchiessgruppeID = id;
                        GruppenName = GruppeResp.Data.Name;
                        if (!Data.Geschlecht.HasValue)
                        {
                            Data.Geschlecht = GruppeResp.Data.Geschlecht;
                        }
                    }
                    RequestIsWorking = false;
                }
            }
        }

        #endregion

        protected override bool CanExecuteSaveCommand() => base.CanExecuteSaveCommand() && Data.SchuetzeID > 0;

        protected override void OnActivated()
        {
            Data = new VereinsmeisterschaftschuetzeErgebnisModel { Geschlecht = null };
        }

    }
}
