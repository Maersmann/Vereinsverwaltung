using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.ViewModels;
using Data.Model.SchnurrschiessenModels;
using Data.Model.SchnurrschiessenModels.DTO;
using Data.Types;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.BaseMessages;
using Logic.Messages.SchnurschiessenMessages;
using Logic.Messages.UserMessages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;
using Prism.Commands;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenMitgliedHistorieUebersichtViewModel : ViewModelUebersicht<SchnurschiessenMitgliedHistorieUebersichtModel, StammdatenTypes>
    {
        private int schnurschiessenStandId;
        public SchnurschiessenMitgliedHistorieUebersichtViewModel()
        {
            MessageToken = "SchnurschiessenMitgliedHistorieUebersicht";
            Title = "Historie Mtiglied";
            schnurschiessenStandId = 0;
            RueckgabeCommand = new RelayCommand(() => new DelegateCommand(ExecuteRueckgabeCommand, CanPost));
            AusgabeCommand = new RelayCommand(() => new DelegateCommand(ExecuteAusgabeCommand, CanPost)); 
            VerlorenCommand = new RelayCommand(() => new DelegateCommand(ExecuteVerlorenCommand, CanPost)); 
            BeschaedigtCommand = new RelayCommand(() => new DelegateCommand(ExecuteBeschaedigtCommand, CanPost));
            RegisterAktualisereViewMessage(StammdatenTypes.schnurschiessen.ToString());

        }

        protected override int GetID() { return SelectedItem.Id; }
        protected override string GetREST_API() { return $"/api/SchnurschiessenMitglied/Uebersicht/MitgliedHistorie?schnurschiessenStandId={schnurschiessenStandId}"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schnurschiessen; }
        protected override bool LoadingOnCreate() { return false; }


        public async void Lade(int schnurschiessenStandId)
        {
            this.schnurschiessenStandId = schnurschiessenStandId;
            await LoadData();
        }

        #region Bindings

        public ICommand RueckgabeCommand { get; set; }
        public ICommand VerlorenCommand { get; set; }
        public ICommand BeschaedigtCommand { get; set; }
        public ICommand AusgabeCommand { get; set; }

        public override SchnurschiessenMitgliedHistorieUebersichtModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
            }
        }
        #endregion
        #region Commands
        private async void ExecuteRueckgabeCommand()
        {          
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Schnurschiessenrang/auszeichnung/rueckgabe", new SchnurschiessenAuszeichnungRueckgabeDTO
                { 
                    MitgliedHistorieID = SelectedItem.Id, 
                    MitgliedID = SelectedItem.MitgliedID, 
                    RangID = SelectedItem.SchnurschiessenrangID 
                });

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                    SendInformationMessage("Gespeichert");
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Konnte nicht gespeichert werden");
                }
                RequestIsWorking = false;

            }
        }

        private async void ExecuteVerlorenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Schnurschiessenrang/auszeichnung/verloren", new SchnurschiessenAuszeichnungVerlorenDTO
                {
                    RangID = SelectedItem.SchnurschiessenrangID
                });

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                    SendInformationMessage("Gespeichert");
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Konnte nicht gespeichert werden");
                }
                RequestIsWorking = false;

            }
        }

        private async void ExecuteBeschaedigtCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Schnurschiessenrang/auszeichnung/beschaedigt", new SchnurschiessenAuszeichnungBeschaedigtDTO
                {
                    RangID = SelectedItem.SchnurschiessenrangID
                });

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                    SendInformationMessage("Gespeichert");
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Konnte nicht gespeichert werden");
                }
                RequestIsWorking = false;

            }
        }

        private async void ExecuteAusgabeCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Schnurschiessenrang/auszeichnung/ausgabe", new SchnurschiessenAuszeichnungAusgabeDTO
                {
                    MitgliedHistorieID = SelectedItem.Id,
                    MitgliedID = SelectedItem.MitgliedID,
                    RangID = SelectedItem.SchnurschiessenrangID
                });

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                    SendInformationMessage("Gespeichert");
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Konnte nicht gespeichert werden");
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
                if (RueckgabeCommand != null)
                {
                    ((DelegateCommand)RueckgabeCommand).RaiseCanExecuteChanged();
                }
                if (AusgabeCommand != null)
                {
                    ((DelegateCommand)AusgabeCommand).RaiseCanExecuteChanged();
                }
                if (VerlorenCommand != null)
                {
                    ((DelegateCommand)VerlorenCommand).RaiseCanExecuteChanged();
                }
                if (BeschaedigtCommand != null)
                {
                    ((DelegateCommand)BeschaedigtCommand).RaiseCanExecuteChanged();
                }
            }
        }

    }
}
