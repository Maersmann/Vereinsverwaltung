using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.MitgliederModels;
using Data.Model.SchnurrschiessenModels;
using Data.Model.SchnurrschiessenModels.DTO;
using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.BaseMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using System.Xml.Linq;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenMitgliederZuordnungViewModel : ViewModelUebersicht<SchnurschiessenMitgliederZuordnungModel, StammdatenTypes>
    {

        public SchnurschiessenMitgliederZuordnungViewModel()
        {
            MessageToken = "SchnurschiessenMitgliederZuordnung";
            Title = "Zuordnung Mitglieder";

            RegisterAktualisereViewMessage(StammdatenTypes.schnurschiessen.ToString());
            ZuordnenCommand = new DelegateCommand(ExecuteZuordnenCommand, CanExecuteCommand);
        }

        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.mitglied; }
        protected override int GetID() { return SelectedItem.MitgliedID; }
        protected override string GetREST_API() { return $"/api/SchnurschiessenMitglied/OhneZuordnung"; }
        protected override bool WithPagination() { return true; }



        #region Bindings

        public ICommand ZuordnenCommand { get; set; }

        #endregion
        #region Commands

        private void ExecuteZuordnenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenMitgliedAuswahlMessage(OpenMitgliedAuswahlCallback), messageToken);
        }

        private async void OpenMitgliedAuswahlCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    RequestIsWorking = true;
                    HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/SchnurschiessenMitglied/Zuordnen", new SchnurschiessenMitgliedZuordnenDTO
                    {
                        KorrektesMitgliedID = id,
                        OhneZuordnungMitgliedID = SelectedItem.MitgliedID
                    });

                    if (resp.IsSuccessStatusCode)
                    {
                        SendInformationMessage("Geändert");
                        await LoadData();
                    }
                    else if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                    {
                        SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                    }
                    else if (!resp.IsSuccessStatusCode)
                    {
                        SendExceptionMessage("Konnte nicht geändert werden.");
                    }
                    RequestIsWorking = false;

                }
            }
        }
        protected override bool CanExecuteCommand()
        {
            return base.CanExecuteCommand() && !RequestIsWorking;
        }
        #endregion

        public override bool RequestIsWorking
        {
            get => base.RequestIsWorking;
            set
            {
                base.RequestIsWorking = value;
                if (ZuordnenCommand != null)
                {
                    ((DelegateCommand)ZuordnenCommand).RaiseCanExecuteChanged();
                }
            }
        }

    }
}
