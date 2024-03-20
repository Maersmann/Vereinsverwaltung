using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.PinModels;
using Data.Types;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.PinViewModels
{
    public class PinsVomMitgliedUebersichtViewModel : ViewModelUebersicht<PinAusgabeMitgliedModel, StammdatenTypes>
    {
        private bool zeigeNurOffene;
        private int id;
        public PinsVomMitgliedUebersichtViewModel()
        {
            id = 0;
            Title = "Übersicht Pins vom Mitglied";
            ErhaltenCommand = new RelayCommand(() => ExecuteErhaltenCommand());
            RueckgaengigCommand = new RelayCommand(() => ExcecuteRueckgaengigCommand());
            zeigeNurOffene = true;
        }
        protected override int GetID() { return SelectedItem.Mitglied.ID; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.mitglied; }
        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/Mitglieder/{id}/pins?nurOffene={zeigeNurOffene}"; }

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
                OnPropertyChanged();
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
                            SendExceptionMessage($"Pin schon erhalten");
                        }
                        else
                        {
                            SendExceptionMessage("Fehler: Pin Erhalten bei ID: " + SelectedItem.Beschreibung);
                            return;
                        }
                    }
                    PinAusgabeMitgliedUebersichtModel content = await resp.Content.ReadAsAsync<PinAusgabeMitgliedUebersichtModel>();
                    SelectedItem.Erhalten = content.Erhalten;
                    SelectedItem.ErhaltenAm = content.ErhaltenAm;
                    LoadData(id);
                }
            }
            catch (Exception e)
            {
                SendExceptionMessage(e.Message);
            }
        }

        #endregion
    }
}