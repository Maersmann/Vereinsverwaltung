using Data.Types;
using GalaSoft.MvvmLight.Messaging;
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

            Messenger.Default.Register<OpenViewMessage>(this, m => ReceiveOpenViewMessage(m));
            Messenger.Default.Register<ExceptionMessage>(this, m => ReceiveExceptionMessage(m));
            Messenger.Default.Register<InformationMessage>(this, m => ReceiveInformationMessage(m));
            Messenger.Default.Register<BaseStammdatenMessage<StammdatenTypes>>(this, m => ReceiceOpenStammdatenMessage(m));
            Messenger.Default.Register<OpenStartingViewMessage>(this, m => ReceiceOpenStartingViewMessage());
            Messenger.Default.Register<OpenLoginViewMessage>(this, m => ReceiceOpenLoginViewMessage());
            Messenger.Default.Register<CloseApplicationMessage>(this, m => ReceiceCloseApplicationMessage());
            Messenger.Default.Register<OpenKonfigurationViewMessage>(this, m => ReceiceOpenKonfigurationViewMessage());
        }   

        private void ReceiceCloseApplicationMessage()
        {
            Application.Current.Shutdown();
        }

        private void ReceiveInformationMessage(InformationMessage m)
        {
            MessageBox.Show(m.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ReceiveExceptionMessage(ExceptionMessage m)
        {
            MessageBox.Show(m.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
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
                default:
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
                case StammdatenTypes.schnur:
                    view = new SchnurstammdatenView();
                    break;
                case StammdatenTypes.schnurauszeichnung:
                    view = new SchnurauszeichnungStammdatenView();
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
                default:
                    break;
            }

            if (view.DataContext is IViewModelStammdaten model)
            {
                if (m.State == State.Bearbeiten)
                {
                    model.ZeigeStammdatenAn(m.ID.Value);
                }

            }
            view.Owner = this;
            view.ShowDialog();
        }

        private void ReceiceOpenKonfigurationViewMessage()
        {
            _ = new KonfigurationView().ShowDialog();
        }

        private void ReceiceOpenLoginViewMessage()
        {
            _ = new LoginView().ShowDialog();
        }

        private void ReceiceOpenStartingViewMessage()
        {
            StartingProgrammView view = new StartingProgrammView();
            _ = view.ShowDialog();
            _ = SchnurschiessenOption.NavigationService.Navigate(new SchnuroptionPage());
        }   
    }

}
