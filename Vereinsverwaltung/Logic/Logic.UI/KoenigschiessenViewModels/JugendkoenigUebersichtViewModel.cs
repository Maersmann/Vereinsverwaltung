using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Types;
using Data.Types.KoenigschiessenTypes;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.KoenigschiessenMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class JugendkoenigUebersichtViewModel : ViewModelUebersicht<JugendkoenigschiessenUebersichtModel, StammdatenTypes>
    {
        public JugendkoenigUebersichtViewModel()
        {
            Title = "Übersicht Jugendkönigsschiessen";
            RegisterAktualisereViewMessage(StammdatenTypes.jugendkoenigschiessen.ToString());
            OeffneAnmeldungCommand = new RelayCommand(() => ExecuteOeffneAnmeldungCommand());
        }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.koenigschiessen; }
        protected override string GetREST_API() { return $"/api/Jugendkoenigschiessen"; }
        protected override bool WithPagination() { return true; }

        #region bindings
        public ICommand OeffneAnmeldungCommand { get; private set; }
        #endregion

        #region Commands
        private void ExecuteOeffneAnmeldungCommand()
        {
            Messenger.Default.Send(new OpenKoenigschiessenAnmeldungViewMessage { Jahr = SelectedItem.Jahr, Variante = KoenigschiessenVarianten.jugendkoenigschiessen }, "JugendkoenigUebersicht");
        }
        #endregion
    }
}
