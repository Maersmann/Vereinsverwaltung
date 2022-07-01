using Data.Model.PinModels;
using Data.Types;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Base.Logic.ViewModels;
using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Wrapper;

namespace Logic.UI.PinViewModels
{
    public class PinAusgabeMitgliedUebersichtViewModel : ViewModelUebersicht<PinAusgabeMitgliedUebersichtModel, StammdatenTypes>
    {
        private bool zeigeNurOffene;
        private int id;
        public PinAusgabeMitgliedUebersichtViewModel()
        {
            id = 0;
            Title = "Übersicht Mitglieder für Pins";
            ErhaltenCommand = new RelayCommand(() => ExecuteErhaltenCommand());
            RueckgaengigCommand = new RelayCommand(() => ExcecuteRueckgaengigCommand());
            zeigeNurOffene = true;
        }
        protected override int GetID() { return SelectedItem.Mitglied.ID; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.mitglied; }
        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/Pins/Ausgabe/Mitglieder/LoadAllForAusgabe/{id}?nurOffene={zeigeNurOffene}"; }

        public void SetFilterData(string filterText, bool zeigeNurOffene)
        {
            if (filterText.Length > 0)
            {
                FilterText = filterText;
            }
            ZeigeNurOffene = zeigeNurOffene;
        }

        protected override bool LoadingOnCreate() => false;

        public override async void LoadData(int id)
        {
            this.id = id;
            await LoadData();
        }


        #region Bindings
        public ICommand ErhaltenCommand { get; private set; }
        public ICommand RueckgaengigCommand { get; private set; }

       
        public bool ZeigeNurOffene
        {
            get => zeigeNurOffene;
            set
            {
                zeigeNurOffene = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        private async void ExcecuteRueckgaengigCommand()
        {
            try
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Pins/Ausgabe/Uebersicht/Mitglied/Rueckgaengig", SelectedItem);
                    if ((int)resp.StatusCode == 901)
                    {
                        SendExceptionMessage($"{SelectedItem.Mitglied.Fullname} hat den Pin schon zurückgegeben");
                    }
                    else if (!resp.IsSuccessStatusCode)
                    {
                        SendExceptionMessage("Fehler: Pin Rückgängig bei ID: " + SelectedItem.Mitglied.Fullname + Environment.NewLine + await resp.Content.ReadAsStringAsync());
                        return;
                    }
                    PinAusgabeMitgliedUebersichtModel content = await resp.Content.ReadAsAsync<PinAusgabeMitgliedUebersichtModel>();
                    SelectedItem.Erhalten = content.Erhalten;
                    SelectedItem.ErhaltenAm = content.ErhaltenAm;
                    FilterText = "";
                    LoadData(id);
                }
            }
            catch (Exception e)
            {
                SendExceptionMessage(e.Message); ;
            }
            
        }
        private async void ExecuteErhaltenCommand()
        {
            try
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Pins/Ausgabe/Uebersicht/Mitglied/Erhalten", SelectedItem);

                    if (!resp.IsSuccessStatusCode)
                    {
                        if ((int)resp.StatusCode == 900)
                        {
                            SendExceptionMessage($"{SelectedItem.Mitglied.Fullname} hat den Pin schon erhalten");
                        }
                        else
                        {
                            SendExceptionMessage("Fehler: Pin Erhalten bei ID: " + SelectedItem.Mitglied.Fullname + Environment.NewLine + await resp.Content.ReadAsStringAsync());
                            return;
                        }
                    }
                    PinAusgabeMitgliedUebersichtModel content = await resp.Content.ReadAsAsync<PinAusgabeMitgliedUebersichtModel>();
                    SelectedItem.Erhalten = content.Erhalten;
                    SelectedItem.ErhaltenAm = content.ErhaltenAm;
                    FilterText = "";
                    LoadData(id);
                }
            }
            catch (Exception e)
            {
                SendExceptionMessage(e.Message);
            }           
        }

        protected override void ExecuteCleanUpCommand()
        {
            Messenger.Default.Send(new AktualisiereViewMessage(), StammdatenTypes.pinAusgabe.ToString());
        }
        #endregion
    }
}
