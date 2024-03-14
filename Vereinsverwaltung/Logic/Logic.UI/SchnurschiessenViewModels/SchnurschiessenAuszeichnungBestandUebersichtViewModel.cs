using Base.Logic.ViewModels;
using Data.Model.SchnurrschiessenModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.SchluesselMessages;
using Logic.Messages.SchnurschiessenMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenAuszeichnungBestandUebersichtViewModel : ViewModelUebersicht<SchnurschiessenAuszeichnungBestandUebersichtModel, StammdatenTypes>
    {

        public SchnurschiessenAuszeichnungBestandUebersichtViewModel()
        {
            MessageToken = "SchnurschiessenAuszeichnungBestandUebersicht";
            Title = "Übersicht Bestand Auszeichnungen";
            RegisterAktualisereViewMessage(StammdatenTypes.schnurschiessenAuszeichnungBestand.ToString());
            OpenGekauftCommand = new RelayCommand(() => ExecuteOpenGekauftCommand());
        }

        protected override int GetID() { return SelectedItem.SchnurauszeichnungId; }
        protected override string GetREST_API() { return $"/api/Schnurschiessenauszeichnung"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schnurschiessenAuszeichnungBestand; }

        #region Bindings

        public ICommand OpenGekauftCommand { get; set; }
        public override SchnurschiessenAuszeichnungBestandUebersichtModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                if (SelectedItem != null)
                {
                    Messenger.Default.Send(new LoadSchnurschiessenAuszeichnungBestandHistorieMessage { schnurschiessenauszeichnungID = SelectedItem.SchnurauszeichnungId }, messageToken);
                }
            }
        }
        #endregion
        #region Commands

        private void ExecuteOpenGekauftCommand()
        {
            Messenger.Default.Send(new OpenAuszeichnungGekauftEintragenMessage { SchnurauszeichnungId = SelectedItem.SchnurauszeichnungId, Bezeichnung = SelectedItem.Bezeichnung }, messageToken);
        }


        #endregion

    }
}
