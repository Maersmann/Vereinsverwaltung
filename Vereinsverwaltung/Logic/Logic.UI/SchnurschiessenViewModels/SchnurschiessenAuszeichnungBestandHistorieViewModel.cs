using Base.Logic.ViewModels;
using Data.Model.SchnurrschiessenModels;
using Data.Types;
using Data.Types.SchnurschiessenTypes;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.SchluesselMessages;
using Logic.Messages.SchnurschiessenMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenAuszeichnungBestandHistorieViewModel : ViewModelUebersicht<SchnurschiessenAuszeichnungBestandHistorieModel, StammdatenTypes>
    {
        private int schnurschiessenauszeichnungID;
        public SchnurschiessenAuszeichnungBestandHistorieViewModel()
        {
            MessageToken = "SchnurschiessenAuszeichnungBestandHistorie";
            Title = "Historie Bestand Auszeichnungen";
            schnurschiessenauszeichnungID = 0;
            Messenger.Default.Register<LoadSchnurschiessenAuszeichnungBestandHistorieMessage>(this, "SchnurschiessenAuszeichnungBestandUebersicht", m => ReceiveLoadSchnurBestandHistorieMessage(m));
        }

        protected override int GetID() { return SelectedItem.Id; }
        protected override string GetREST_API() { return $"/api/Schnurschiessenauszeichnung/Historie?schnurschiessenauszeichnungID={schnurschiessenauszeichnungID}"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schnurschiessen; }
        protected override bool LoadingOnCreate() { return false; }


        public async void Lade(int schnurschiessenauszeichnungID)
        {
            this.schnurschiessenauszeichnungID = schnurschiessenauszeichnungID;
            await LoadData();
        }

        #region Bindings

        public override SchnurschiessenAuszeichnungBestandHistorieModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
            }
        }
        #endregion
        #region Commands

        private async void ReceiveLoadSchnurBestandHistorieMessage(LoadSchnurschiessenAuszeichnungBestandHistorieMessage m)
        {
            schnurschiessenauszeichnungID = m.schnurschiessenauszeichnungID;
            await LoadData();
        }

        #endregion

    }
}
