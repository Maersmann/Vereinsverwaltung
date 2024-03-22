using Base.Logic.Core;
using Base.Logic.ViewModels;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Data.Model.MitgliederModels;
using Data.Types.MitgliederTypes;
using Data.Types;
using Logic.Messages.KoenigschiessenMessages;
using Logic.Messages.PinMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Logic.Messages;
using Logic.Messages.BaseMessages;
using System.Net.Http.Json;
using Prism.Commands;

namespace Logic.UI.MitgliederViewModels
{
    public class MitgliederAnonymisierenViewModel : ViewModelUebersicht<MitgliederAnonymisierenModel, StammdatenTypes>
    {

        public MitgliederAnonymisierenViewModel()
        {
            Title = "Übersicht Ehemailige Mitglieder";
            RegisterAktualisereViewMessage(StammdatenTypes.EhemaligeMitglieder.ToString());
            AnonymisierenCommand = new DelegateCommand(ExcecuteAnonymisierenCommand, CanPost);
        }


        protected override int GetID() { return 0; }
        protected override string GetREST_API() { return "/api/Mitglieder/anonymisieren"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.EhemaligeMitglieder; }

        #region bindings
        public ICommand AnonymisierenCommand { get; private set; }
        #endregion
        #region Commands

        

        private async void ExcecuteAnonymisierenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsync(GlobalVariables.BackendServer_URL + $"/api/Mitglieder/anonymisieren", null);
                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                    await LoadData();
                }
                else
                {   
                    SendExceptionMessage("Mitglied konnte nicht gespeichert werden.");
                }
                RequestIsWorking = false;
            }
        }

        public bool CanPost() => !RequestIsWorking;
        #endregion

        public override bool RequestIsWorking
        {
            get => base.RequestIsWorking;
            set
            {
                base.RequestIsWorking = value;
                if (AnonymisierenCommand != null)
                {
                    ((DelegateCommand)AnonymisierenCommand).RaiseCanExecuteChanged();
                }
            }
        }

    }
}
