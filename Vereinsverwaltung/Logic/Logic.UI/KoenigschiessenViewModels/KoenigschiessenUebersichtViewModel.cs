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
    public class KoenigschiessenUebersichtViewModel : ViewModelUebersicht<KoenigschiessenUebersichtModel, StammdatenTypes>
    {
        public KoenigschiessenUebersichtViewModel()
        {
            Title = "Übersicht Königsschiessen";
            RegisterAktualisereViewMessage(StammdatenTypes.koenigschiessen.ToString());
            OeffneAnmeldungCommand = new RelayCommand(() => ExecuteOeffneAnmeldungCommand());
            OeffneKoenigschiessenCommand = new RelayCommand(() => ExecuteOeffneRundeCommand(KoenigschiessenArt.koenig));
            OeffneVizeKoenigschiessenCommand = new RelayCommand(() => ExecuteOeffneRundeCommand(KoenigschiessenArt.vizekoenig));
            OeffneBesteSchuetzinCommand = new RelayCommand(() => ExecuteOeffneRundeCommand(KoenigschiessenArt.besteSchuetzin));
            OeffneZahlenCommand = new RelayCommand(() => ExecuteOeffneZahlenCommand());
        }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.koenigschiessen; }
        protected override string GetREST_API() { return $"/api/Koenigschiessen"; }
        protected override bool WithPagination() { return true; }

        #region bindings
        public ICommand OeffneAnmeldungCommand { get; private set; }
        public ICommand OeffneKoenigschiessenCommand { get; private set; }
        public ICommand OeffneVizeKoenigschiessenCommand { get; private set; }
        public ICommand OeffneBesteSchuetzinCommand { get; private set; }
        public ICommand OeffneZahlenCommand { get; private set; }
        #endregion

        #region Commands
        private void ExecuteOeffneAnmeldungCommand()
        {
            Messenger.Default.Send(new OpenKoenigschiessenAnmeldungViewMessage { Jahr = SelectedItem.Jahr, Variante = KoenigschiessenVarianten.koenigschiessen }, "KoenigschiessenUebersicht");
        }

        private void ExecuteOeffneZahlenCommand()
        {
            Messenger.Default.Send(new OpenKoenigschiessenZahlenMessage { Jahr = SelectedItem.Jahr, Variante = KoenigschiessenVarianten.koenigschiessen }, "KoenigschiessenUebersicht");
        }

        private void ExecuteOeffneRundeCommand(KoenigschiessenArt art)
        {
            int Runde;
            switch (art)
            {
                case KoenigschiessenArt.besteSchuetzin:
                    Runde = SelectedItem.RundeBesteSchuetzin;
                    break;
                case KoenigschiessenArt.vizekoenig:
                    Runde = SelectedItem.RundeVizekoenigschiessen;
                    break;
                case KoenigschiessenArt.koenig:
                    Runde = SelectedItem.RundeKoenigschiessen;
                    break;
                default:
                    return;
            }
            Messenger.Default.Send(new OpenKoenigschiessenRundeTeilnehmerUebersichtViewMessage { Jahr = SelectedItem.Jahr, Variante = KoenigschiessenVarianten.koenigschiessen, Runde = Runde, Art = art }, "KoenigschiessenUebersicht");
        }
        #endregion
    }
}
