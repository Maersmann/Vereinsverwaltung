using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Types;
using Data.Types.KoenigschiessenTypes;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
            OeffneJugendkoenigschiessenCommand = new RelayCommand(() => ExecuteOeffneRundeCommand(KoenigschiessenArt.jugendkoenig));
            OeffneJugendkoeniginschiessenCommand = new RelayCommand(() => ExecuteOeffneRundeCommand(KoenigschiessenArt.jugendkoenigin));
            OeffneZahlenCommand = new RelayCommand(() => ExecuteOeffneZahlenCommand());
        }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.koenigschiessen; }
        protected override string GetREST_API() { return $"/api/Jugendkoenigschiessen"; }
        protected override bool WithPagination() { return true; }

        #region bindings
        public ICommand OeffneAnmeldungCommand { get; private set; }
        public ICommand OeffneJugendkoenigschiessenCommand { get; private set; }
        public ICommand OeffneJugendkoeniginschiessenCommand { get; private set; }
        public ICommand OeffneZahlenCommand { get; private set; }
        #endregion

        #region Commands
        private void ExecuteOeffneAnmeldungCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenKoenigschiessenAnmeldungViewMessage { Jahr = SelectedItem.Jahr, Variante = KoenigschiessenVarianten.jugendkoenigschiessen }, "JugendkoenigUebersicht");
        }

        private void ExecuteOeffneZahlenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenKoenigschiessenZahlenMessage { Jahr = SelectedItem.Jahr, Variante = KoenigschiessenVarianten.jugendkoenigschiessen }, "JugendkoenigUebersicht");
        }

        private void ExecuteOeffneRundeCommand(KoenigschiessenArt art)
        {
            int Runde;
            switch (art)
            {
                case KoenigschiessenArt.jugendkoenig:
                    Runde = SelectedItem.RundeJugendkoenig;
                    break;
                case KoenigschiessenArt.jugendkoenigin:
                    Runde = SelectedItem.RundeJugendkoenigin;
                    break;
                default:
                    return;
            }
            WeakReferenceMessenger.Default.Send(new OpenKoenigschiessenRundeTeilnehmerUebersichtViewMessage { Jahr = SelectedItem.Jahr, Variante = KoenigschiessenVarianten.jugendkoenigschiessen, Runde = Runde, Art = art }, "JugendkoenigUebersicht");
        }
        #endregion
    }
}
