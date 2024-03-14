using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.SchnurrschiessenModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.SchluesselMessages;
using Logic.Messages.SchnurschiessenMessages;
using Logic.Messages.VereinsmeisterschaftMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenMitgliederUebersichtViewModel : ViewModelUebersicht<SchnurschiessenMitgliederUebersichtModel, StammdatenTypes>
    {

        public SchnurschiessenMitgliederUebersichtViewModel()
        {
            MessageToken = "SchnurschiessenMitgliederUebersicht";
            Title = "Aktueller Stand Schnurschießen";
            RegisterAktualisereViewMessage(StammdatenTypes.schnurschiessen.ToString());
            OpenHistorieCommand = new DelegateCommand(ExecuteOpenHistorieCommand, CanExecuteCommand);
        }

        protected override int GetID() { return SelectedItem.Id; }
        protected override string GetREST_API() { return $"/api/SchnurschiessenMitglied/Uebersicht/AktuellenStand"; }
        protected override bool WithPagination() { return true; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.schnurschiessen; }

        #region Bindings

        public ICommand OpenHistorieCommand { get; set; }

        public override SchnurschiessenMitgliederUebersichtModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
            }
        }
        #endregion
        #region Commands

        private void ExecuteOpenHistorieCommand()
        {
            Messenger.Default.Send(new OpenSchnurschiessenMitgliedHistorieUebersicht { Id = SelectedItem.Id }, messageToken);
        }

        #endregion

    }
}
