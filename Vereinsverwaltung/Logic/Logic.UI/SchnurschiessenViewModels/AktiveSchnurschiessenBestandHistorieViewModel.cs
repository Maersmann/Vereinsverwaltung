using Base.Logic.ViewModels;
using Data.Model.SchnurrschiessenModels;
using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.SchnurschiessenMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class AktiveSchnurschiessenBestandHistorieViewModel : ViewModelUebersicht<AktiveSchnurschiessenBestandHistorieModel, StammdatenTypes>
    {
        private int schnurschiessenBestandID;
        public AktiveSchnurschiessenBestandHistorieViewModel()
        {
            MessageToken = "AktiveSchnurschiessenBestandHistorie";
            Title = "Historie Bestand Auszeichnungen";
            schnurschiessenBestandID = 0;
            WeakReferenceMessenger.Default.Register<LoadAktiveSchnurschiessenBestandHistorieMessage, string>(this, "AktiveSchnurschiessenVerwaltung", (r,m) => ReceiveLoadAktiveSchnurschiessenBestandHistorieMessage(m));
        }

        protected override int GetID() { return SelectedItem.ID; }
        protected override string GetREST_API() { return $"/api/Schnurschiessen/AuszeichnungBestand/Historie?schnurschiessenBestandID={schnurschiessenBestandID}"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schnurschiessen; }
        protected override bool LoadingOnCreate() { return false; }


        public async void Lade(int schnurschiessenBestandID)
        {
            this.schnurschiessenBestandID = schnurschiessenBestandID;
            await LoadData();
        }

        #region Bindings

        public override AktiveSchnurschiessenBestandHistorieModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
            }
        }
        #endregion
        #region Commands

        private async void ReceiveLoadAktiveSchnurschiessenBestandHistorieMessage(LoadAktiveSchnurschiessenBestandHistorieMessage m)
        {
            if(m.SchnurschiessenBestandID == 0)
            {
                ItemList.Clear();
                OnPropertyChanged(nameof(ItemList));
            }
            else
            {
                schnurschiessenBestandID = m.SchnurschiessenBestandID;
                await LoadData();
            }
        }

        #endregion
    } 
}
