using Data.Model.PinModels;
using Data.Types;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.BaseMessages;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Logic.UI.PinViewModels
{
    public class PinAusgabeMitgliedUebersichtViewModel : ViewModelUebersicht<PinAusgabeMitgliedUebersichtModel>
    {
        private string filtertext;
        private bool zeigeNurNichtErhalten;
        private int id;
        public PinAusgabeMitgliedUebersichtViewModel()
        {
            id = 0;
            Title = "Übersicht Mitglieder für Pins";
            ErhaltenCommand = new RelayCommand(() => ExecuteErhaltenCommand());
            RueckgaengigCommand = new RelayCommand(() => ExcecuteRueckgaengigCommand());
            filtertext = "";
            zeigeNurNichtErhalten = true;
        }
        protected override int GetID() { return selectedItem.ID; }

        public async override void LoadData(int id)
        {
            this.id = id;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins/Ausgabe/Mitglieder/LoadAllForAusgabe/{this.id}");
                if (resp.IsSuccessStatusCode)
                    itemList = await resp.Content.ReadAsAsync<ObservableCollection<PinAusgabeMitgliedUebersichtModel>>();
            }
            base.LoadData();
        }

        protected override bool OnFilterTriggered(object item)
        {
            if (item is PinAusgabeMitgliedUebersichtModel mitglied)
            {
                var MitgliedsNr = Convert.ToString(mitglied.Mitglied.Mitgliedsnr);
                if (zeigeNurNichtErhalten)
                    return (mitglied.Mitglied.Fullname.Contains(filtertext) || MitgliedsNr.Contains(filtertext)) && !mitglied.Erhalten;
                else
                    return mitglied.Mitglied.Fullname.Contains(filtertext) || MitgliedsNr.Contains(filtertext);
            }
            return true;
        }

        #region Bindings
        public ICommand ErhaltenCommand { get; private set; }
        public ICommand RueckgaengigCommand { get; private set; }
        public String FilterText
        {
            get => this.filtertext;
            set
            {
                this.filtertext = value;
                this.RaisePropertyChanged();
                _customerView.Refresh();
            }
        }
        
        public Boolean ZeigeNurNichtErhalten
        {
            get
            {
                return zeigeNurNichtErhalten;
            }
            set
            {
                zeigeNurNichtErhalten = value;
                this.RaisePropertyChanged();
                _customerView.Refresh();
            }
        }
        
        #endregion

        #region Commands

        private async void ExcecuteRueckgaengigCommand()
        {
            
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins/Ausgabe/Uebersicht/Mitglied/Rueckgaengig", SelectedItem );
                if ((int)resp.StatusCode == 901)
                {
                    SendExceptionMessage("Mitglied hat Pin schon zurückgegeben");
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Fehler: Pin Rückgängig bei ID: " + SelectedItem.Mitglied.Fullname + Environment.NewLine + await resp.Content.ReadAsStringAsync());
                    return;
                }
                var content = await resp.Content.ReadAsAsync<PinAusgabeMitgliedUebersichtModel>();
                selectedItem.Erhalten = content.Erhalten;
                selectedItem.ErhaltenAm = content.ErhaltenAm;
                base.LoadData();

            }
        }
        private async void ExecuteErhaltenCommand()
        {
            
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Pins/Ausgabe/Uebersicht/Mitglied/Erhalten", SelectedItem);

                if (!resp.IsSuccessStatusCode)
                {
                    if ((int)resp.StatusCode == 900)
                    {
                        SendExceptionMessage("Mitglied hat Pin schon erhalten");
                    }
                    else
                    { 
                        SendExceptionMessage("Fehler: Pin Erhalten bei ID: " + SelectedItem.Mitglied.Fullname + Environment.NewLine + await resp.Content.ReadAsStringAsync());
                        return;
                    }
                }
                var content = await resp.Content.ReadAsAsync<PinAusgabeMitgliedUebersichtModel>();
                selectedItem.Erhalten = content.Erhalten;
                selectedItem.ErhaltenAm = content.ErhaltenAm;
                base.LoadData();
            }
            
        }

        protected override void ExecuteCleanUpCommand()
        {
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.pinAusgabe);
        }
        #endregion
    }
}
