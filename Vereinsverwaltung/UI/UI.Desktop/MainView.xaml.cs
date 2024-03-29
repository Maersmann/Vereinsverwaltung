﻿using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using System.Windows;
using UI.Desktop;
using UI.Desktop.BaseViews;
using UI.Desktop.Mitglieder;
using Vereinsverwaltung.UI.Desktop.Mitglieder;
using UI.Desktop.Schluesselverwaltung;
using Vereinsverwaltung.UI.Desktop.Schluesselverwaltung.Pages;
using Vereinsverwaltung.UI.Desktop.Schnurschiessen.Pages;
using Vereinsverwaltung.UI.Desktop.Schluesselverwaltung;
using UI.Desktop.Schnurschiessen;
using UI.Desktop.Pin;
using UI.Desktop.Auswertungen;
using UI.Desktop.Konfiguration;
using Base.Logic.Messages;
using Base.Logic.Types;
using UI.Desktop.Export;
using UI.Desktop.KkSchiessen;
using UI.Desktop.Vereinsmeisterschaft;
using UI.Desktop.Vereinsmeisterschaft.Pages;
using UI.Desktop.User;
using UI.Desktop.Auswertungen.Mitglieder;
using Logic.Messages.UserMessages;
using UI.Desktop.Koenigschiessen;
using UI.Desktop.Schnurschiessen.Pages;
using UI.Desktop.Auswertungen.Schnurschiessen;
using UI.Desktop.Schuetzenfest;
using UI.Desktop.Auswertungen.Schuetzenfestzahlen;

namespace Vereinsverwaltung.UI.Desktop
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainView
    {

        private static MitgliederUebersichtView mitgliederUebersichtView;
        private static MitgliederImportView mitgliederImportView;
        private static SchluesselUebersichtView schluesselUebersicht;
        private static SchluesselbesitzerUebersichtView schluesselbesitzerUebersicht;
        private static SchluesselverteilungBesitzerUebersichtPage schluesselzuteilungBesitzerUebersicht;
        private static SchluesselverteilungSchluesselUebersichtPage schluesselverteilungSchluesselUebersicht;
        private static SchluesselverteilungFreieSchluesselUebersichtView schluesselverteilungFreieSchluesselUebersicht;
        private static PinAusgabeUebersichtView pinAusgabeUebersichtView;
        public MainView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<OpenViewMessage>(this, (r, m) => ReceiveOpenViewMessage(m));
            WeakReferenceMessenger.Default.Register<ExceptionMessage>(this, (r, m) => ReceiveExceptionMessage(m));
            WeakReferenceMessenger.Default.Register<InformationMessage>(this, (r, m) => ReceiveInformationMessage(m));
            WeakReferenceMessenger.Default.Register<BaseStammdatenMessage<StammdatenTypes>>(this, (r, m) => ReceiceOpenStammdatenMessage(m));
            WeakReferenceMessenger.Default.Register<OpenStartingViewMessage>(this, (r, m) => ReceiceOpenStartingViewMessage());
            WeakReferenceMessenger.Default.Register<OpenLoginViewMessage>(this, (r, m) => ReceiceOpenLoginViewMessage());
            WeakReferenceMessenger.Default.Register<CloseApplicationMessage>(this, (r, m) => ReceiceCloseApplicationMessage());
            WeakReferenceMessenger.Default.Register<OpenKonfigurationViewMessage>(this, (r, m) => ReceiceOpenKonfigurationViewMessage());
            WeakReferenceMessenger.Default.Register<OpenPasswordAendernViewMessage>(this, (r, m) => ReceiceOpenPasswordAendernViewMessage());       
        }

        private static void ReceiceCloseApplicationMessage()
        {
            Application.Current.Shutdown();
        }

        private void ReceiveInformationMessage(InformationMessage m)
        {
            MessageBox.Show(this, m.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ReceiveExceptionMessage(ExceptionMessage m)
        {
            MessageBox.Show(this, m.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ReceiveOpenViewMessage(OpenViewMessage m)
        {
            Naviagtion(m.ViewType);
        }

        public void Naviagtion(ViewType inType)
        {
            switch (inType)
            {
                case ViewType.viewMitgliederUebersicht:
                    mitgliederUebersichtView ??= new MitgliederUebersichtView();
                    Container.NavigationService.Navigate(mitgliederUebersichtView);
                    break;
                case ViewType.viewMitgliederImport:
                    mitgliederImportView ??= new MitgliederImportView();
                    Container.NavigationService.Navigate(mitgliederImportView);
                    break;
                case ViewType.viewSchluesselUebersicht:
                    schluesselUebersicht ??= new SchluesselUebersichtView();
                    Container.NavigationService.Navigate(schluesselUebersicht);
                    break;
                case ViewType.viewSchluesselbesitzerUebersicht:
                    schluesselbesitzerUebersicht ??= new SchluesselbesitzerUebersichtView();
                    Container.NavigationService.Navigate(schluesselbesitzerUebersicht);
                    break;
                case ViewType.viewZuteilungSchluesselbesitzerUebersicht:
                    schluesselzuteilungBesitzerUebersicht ??= new SchluesselverteilungBesitzerUebersichtPage();
                    Container.NavigationService.Navigate(schluesselzuteilungBesitzerUebersicht);
                    break;
                case ViewType.viewZuteilungSchluesselUebersicht:
                    schluesselverteilungSchluesselUebersicht ??= new SchluesselverteilungSchluesselUebersichtPage();
                    Container.NavigationService.Navigate(schluesselverteilungSchluesselUebersicht);
                    break;
                case ViewType.viewZuteilungFreieAnzahlUebersicht:
                    schluesselverteilungFreieSchluesselUebersicht ??= new SchluesselverteilungFreieSchluesselUebersichtView();
                    Container.NavigationService.Navigate(schluesselverteilungFreieSchluesselUebersicht);
                    break;
                case ViewType.viewPinAusgabeUebersicht:
                    pinAusgabeUebersichtView ??= new PinAusgabeUebersichtView();
                    Container.NavigationService.Navigate(pinAusgabeUebersichtView);
                    break;
                case ViewType.viewAuswertungPinAusgabeTag:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(PinAusgabeAuswertungTagView).Name))
                        Container.NavigationService.Navigate(new PinAusgabeAuswertungTagView());
                    break;
                case ViewType.viewAuswertungPinAusgabeTagStunde:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(PinAusgabeAuswertungTagStundeView).Name))
                        Container.NavigationService.Navigate(new PinAusgabeAuswertungTagStundeView());
                    break;
                case ViewType.viewExportSchluessel:
                    new ExportSchluesselView().ShowDialog();
                    break;
                case ViewType.viewExportMitgliederAenderungen:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(ExportMitgliederAenderungenView).Name))
                        Container.NavigationService.Navigate(new ExportMitgliederAenderungenView());
                    break;
                case ViewType.viewKkSchiessenUebersicht:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(KkSchiessenUebersichtView).Name))
                        Container.NavigationService.Navigate(new KkSchiessenUebersichtView());
                    break;
                case ViewType.viewKkSchiessgruppeUebersicht:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(KkSchiessgruppeUebersichtView).Name))
                        Container.NavigationService.Navigate(new KkSchiessgruppeUebersichtView());
                    break;
                case ViewType.viewAuswertungKkSchiessenMonat:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(KkSchiessenMonatAuswertungView).Name))
                        Container.NavigationService.Navigate(new KkSchiessenMonatAuswertungView());
                    break;
                case ViewType.viewAuswertungKkSchiessenMonatJahresvergleich:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(KkSchiessenMonatJahresvergleichAuswertungView).Name))
                        Container.NavigationService.Navigate(new KkSchiessenMonatJahresvergleichAuswertungView());
                    break;
                case ViewType.viewVereinsmeisterschaftAktiveVereinsmeisterschaft:
                    if(Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(VereinsmeisterschaftAktivView).Name))
                        Container.NavigationService.Navigate(new VereinsmeisterschaftAktivView());
                    break;
                case ViewType.viewSchuetzenUebersicht:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchuetzenUebersichtView).Name))
                        Container.NavigationService.Navigate(new SchuetzenUebersichtView());
                    break;
                case ViewType.viewSchiessgruppenUebersicht:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchiessgruppenUebersichtView).Name))
                        Container.NavigationService.Navigate(new SchiessgruppenUebersichtView());
                    break;
                case ViewType.viewExportVereinsmeisterschaft:
                    new ExportVereinsmeisterschaftView().ShowDialog();
                    break;
                case ViewType.viewVereinsmeisterschaftAktiveVereinsmeisterschaftErgebnisseSchuetzen:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(VereinsmeisterschaftAktivErgebnisseSchuetzenPage).Name))
                        Container.NavigationService.Navigate(new VereinsmeisterschaftAktivErgebnisseSchuetzenPage());
                    break;
                case ViewType.viewVereinsmeisterschaftAktiveVereinsmeisterschaftErgebnisseGruppen:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(VereinsmeisterschaftAktivErgebnisseGruppenPage).Name))
                        Container.NavigationService.Navigate(new VereinsmeisterschaftAktivErgebnisseGruppenPage());
                    break;
                case ViewType.viewVereinsmeisterschaftenUebersicht:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(VereinsmeisterschaftenUebersichtView).Name))
                        Container.NavigationService.Navigate(new VereinsmeisterschaftenUebersichtView());
                    break;
                case ViewType.viewAuswertungVereinsmeisterschaftEntwicklungGruppen:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(AuswertungVereinsmeisterschaftEntwicklungGruppenView).Name))
                        Container.NavigationService.Navigate(new AuswertungVereinsmeisterschaftEntwicklungGruppenView());
                    break;
                case ViewType.viewAuswertungVereinsmeisterschaftEntwicklungSchuetzen:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(AuswertungVereinsmeisterschaftEntwicklungSchuetzenView).Name))
                        Container.NavigationService.Navigate(new AuswertungVereinsmeisterschaftEntwicklungSchuetzenView());
                    break;
                case ViewType.viewAuswertungMitgliederAuswertungEintritt:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(MitgliederAuswertungEintrittView).Name))
                        Container.NavigationService.Navigate(new MitgliederAuswertungEintrittView());
                    break;
                case ViewType.viewAuswertungMitgliederAuswertungJahreImVerein:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(MitgliederAuswertungJahreImVereinView).Name))
                        Container.NavigationService.Navigate(new MitgliederAuswertungJahreImVereinView());
                    break;
                case ViewType.viewAuswertungMitgliederAuswertungJahrgang:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(MitgliederAuswertungJahrgangView).Name))
                        Container.NavigationService.Navigate(new MitgliederAuswertungJahrgangView());
                    break;
                case ViewType.viewAuswertungMitgliederAuswertungJahrzehnt:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(MitgliederAuswertungJahrzehnteView).Name))
                        Container.NavigationService.Navigate(new MitgliederAuswertungJahrzehnteView());
                    break;
                case ViewType.viewKoenigschiessenUebersicht:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(KoenigschiessenUebersichtView).Name))
                        Container.NavigationService.Navigate(new KoenigschiessenUebersichtView());
                    break;
                case ViewType.viewJugendkoenigschiessenUebersicht:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(JugendkoenigschiessenUebersichtView).Name))
                        Container.NavigationService.Navigate(new JugendkoenigschiessenUebersichtView());
                    break;
                case ViewType.viewSchnurschiessenMitgliederUebersicht:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenMitgliederUebersichtView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenMitgliederUebersichtView());
                    break;
                case ViewType.viewSchnurschiessenAuszeichnungBestandHistorie:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenAuszeichnungBestandHistoriePage).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenAuszeichnungBestandHistoriePage());
                    break;
                case ViewType.viewAktiveSchnurschiessenVerwaltung:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(AktiveSchnurschiessenVerwaltungPage).Name))
                        Container.NavigationService.Navigate(new AktiveSchnurschiessenVerwaltungPage());
                    break;
                case ViewType.viewAktivesSchnurschiessenMitgliederUebersicht:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(AktivesSchnurschiessenMitgliederUebersichtView).Name))
                        Container.NavigationService.Navigate(new AktivesSchnurschiessenMitgliederUebersichtView());
                    break;
                case ViewType.viewSchnurschiessenMitgliederImport:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenMitgliederImportView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenMitgliederImportView());
                    break;
                case ViewType.viewSchnurschiessenAuswertungAktuellenStandAuszeichnung:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenAuswertungAktuellenStandAuszeichnungView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenAuswertungAktuellenStandAuszeichnungView());
                    break;
                case ViewType.viewSchnurschiessenAuswertungAktuellenStandRang:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenAuswertungAktuellenStandRangView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenAuswertungAktuellenStandRangView());
                    break;
                case ViewType.viewSchnurschiessenAuswertungEntwicklungAuszeichnung:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenAuswertungEntwicklungAuszeichnungView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenAuswertungEntwicklungAuszeichnungView());
                    break;
                case ViewType.viewSchnurschiessenAuswertungEntwicklungRang:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenAuswertungEntwicklungRangView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenAuswertungEntwicklungRangView());
                    break;
                case ViewType.viewSchnurschiessenAuswertungGesamtteilnahme:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenAuswertungGesamtteilnahmeView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenAuswertungGesamtteilnahmeView());
                    break;
                case ViewType.viewSchnurschiessenAuswertungNeuerRang:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenAuswertungNeuerRangView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenAuswertungNeuerRangView());
                    break;
                case ViewType.viewSchnurschiessenAuswertungTeilnahmeProTag:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenAuswertungTeilnahmeProTagView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenAuswertungTeilnahmeProTagView());
                    break;
                case ViewType.viewSchnurschiessenAuswertungErhalteneAuszeichnung:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenAuswertungErhalteneAuszeichnungView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenAuswertungErhalteneAuszeichnungView());
                    break;
                case ViewType.viewSchnurschiessenMitgliederZuordnung:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchnurschiessenMitgliederZuordnungView).Name))
                        Container.NavigationService.Navigate(new SchnurschiessenMitgliederZuordnungView());
                    break;                
                case ViewType.viewExportSchnurschiessen:
                    var View = new ExportSchnurschiessenView
                    {
                        Owner = this
                    };
                    View.ShowDialog();
                    break;
                case ViewType.viewMitgliederAnonymisieren:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(MitgliederAnonymisierenView).Name))
                        Container.NavigationService.Navigate(new MitgliederAnonymisierenView());
                    break;
                case ViewType.viewSchuetzenfestZahlenUebersicht:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchuetzenfestZahlenUebersichtView).Name))
                        Container.NavigationService.Navigate(new SchuetzenfestZahlenUebersichtView());
                    break;
                case ViewType.viewSchuetzenfestZahlenAuswertungBaendchen:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchuetzenfestZahlenAuswertungBaendchenView).Name))
                        Container.NavigationService.Navigate(new SchuetzenfestZahlenAuswertungBaendchenView());
                    break;
                case ViewType.viewSchuetzenfestZahlenAuswertungUmzug:
                    if (Container.Content == null || !Container.Content.GetType().Name.Equals(typeof(SchuetzenfestZahlenAuswertungUmzugView).Name))
                        Container.NavigationService.Navigate(new SchuetzenfestZahlenAuswertungUmzugView());
                    break;
                default:         
                    Container.Content = null;
                    Container.NavigationService.RemoveBackEntry();
                    break;
            }
        }


        private void ReceiceOpenStammdatenMessage(BaseStammdatenMessage<StammdatenTypes> m)
        {
            StammdatenView view = null;
            switch ( m.Stammdaten)
            {
                case StammdatenTypes.mitglied:
                    view = new MitgliedStammdatenView();
                    break;
                case StammdatenTypes.schluessel:
                    view = new SchluesselStammdatenView();
                    break;
                case StammdatenTypes.schluesselbesitzer:
                    view = new SchluesselbesitzerStammdatenView();
                    break;
                case StammdatenTypes.schnurschiessenAuszeichnung:
                    view = new SchnurschiessenAuszeichnungStammdatenView();
                    break;
                case StammdatenTypes.schnurschiessenRang:
                    view = new SchnurschiessenrangStammdatenView();
                    break;
                case StammdatenTypes.pinAusgabe:
                    view = new PinAusgabeStammdatenView();
                    break;
                case StammdatenTypes.kkSchiessgruppe:
                    view = new KkSchiessgruppeStammdatenView();
                    break;
                case StammdatenTypes.kkSchiessen:
                    view = new KKSchiessenStammdatenView();
                    break;
                case StammdatenTypes.schuetze:
                    view = new SchuetzeStammdatenView();
                    break;
                case StammdatenTypes.schiessgruppe:
                    view = new SchiessgruppeStammdatenView();
                    break;
                case StammdatenTypes.user:
                    view = new UserStammdatenView();
                    break;
                case StammdatenTypes.koenigschiessen:
                    view = new KoenigschiessenErstellenView();
                    break;
                case StammdatenTypes.jugendkoenigschiessen:
                    view = new JugendkoenigschiessenErstellenView();
                    break;
                case StammdatenTypes.schuetzenfestZahlen:
                    view = new SchuetzenfestZahlenStammdatenView();
                    break;
                default:
                    break;
            }

            if (view.DataContext is IViewModelStammdaten model)
            {
                if (m.State == State.Bearbeiten)
                {
                    model.ZeigeStammdatenAnAsync(m.ID.Value);
                }

            }
            view.Owner = this;
            view.ShowDialog();
        }

        private static void ReceiceOpenKonfigurationViewMessage()
        {
            _ = new KonfigurationView().ShowDialog();
        }

        private void ReceiceOpenLoginViewMessage()
        {
            LoginView view = new()
            {
                Owner = this
            };
            view.ShowDialog();
        }

        private void ReceiceOpenStartingViewMessage()
        {
            StartingProgrammView view = new();
            _ = view.ShowDialog();
            _ = SchnurschiessenOption.NavigationService.Navigate(new SchnurschiessenOptionPage());
        }

        private static void ReceiceOpenPasswordAendernViewMessage()
        {
            UserPasswordAendernView view = new();
            _ = view.ShowDialog();
        }
    }

}
