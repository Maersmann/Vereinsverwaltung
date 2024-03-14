/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:UI.Desktop"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using Logic.UI.AuswahlViewModels;
using Logic.UI.MitgliederViewModels;
using Logic.UI.SchluesselverwaltungViewModels;
using Logic.UI.SchnurschiessenViewModels;
using Logic.UI.PinViewModels;
using Logic.UI.AuswertungenViewModels;
using Logic.UI.OptionenViewModels;
using Logic.UI.KonfigruationViewModels;
using Logic.UI.UtilsViewModels;
using Logic.UI.ExportViewModels;
using Logic.UI.KkSchiessenViewModels;
using Logic.UI.VereinsmeisterschaftViewModels;
using Logic.UI.UserViewModels;
using Logic.UI.AuswertungenViewModels.MitgliederAuswertungenViewModels;
using Logic.UI.KoenigschiessenViewModels;
using Logic.UI.AuswertungenViewModels.SchnurschiessenAuswertungenViewModels;

namespace Logic.UI
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models                
            }
            else
            {
                // Create run time view services and models                
            }
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MitgliederStammdatenViewModel>();
            SimpleIoc.Default.Register<MitgliederUebersichtViewModel>();
            SimpleIoc.Default.Register<MitgliederImportViewModel>();
            SimpleIoc.Default.Register<SchluesselStammdatenViewModel>();
            SimpleIoc.Default.Register<SchluesselUebersichtViewModel>();
            SimpleIoc.Default.Register<SchluesselzuteilungStammdatenViewModel>();
            SimpleIoc.Default.Register<SchluesselbesitzerAuswahlViewModel>();
            SimpleIoc.Default.Register<SchluesselbesitzerStammdatenViewModel>();
            SimpleIoc.Default.Register<MitgliedAuswahlViewModel>();
            SimpleIoc.Default.Register<SchluesselbesitzerUebersichtViewModel>();
            SimpleIoc.Default.Register<SchluesselAuswahlViewModel>();
            SimpleIoc.Default.Register<SchluesselverteilungBesitzerUebersichtViewModel>();
            SimpleIoc.Default.Register<SchluesselverteilungBesitzerUebersichtDetailViewModel>();
            SimpleIoc.Default.Register<SchluesselverteilungSchluesselUebersichtViewModel>();
            SimpleIoc.Default.Register<SchluesselverteilungSchluesselUebersichtDetailViewModel>();
            SimpleIoc.Default.Register<SchluesselverteilungFreieSchluesselUebersichtViewModel>();
            SimpleIoc.Default.Register<SchluesselRueckgabeStammdatenViewModel>();
            SimpleIoc.Default.Register<SchluesselzuteilungAuswahlViewModel>();
            SimpleIoc.Default.Register<SchluesselzuteilungHistoryUebersichtViewModel>();
            SimpleIoc.Default.Register<BackendSettingsViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public MitgliederStammdatenViewModel MitgliederStammdaten => ServiceLocator.Current.GetInstance<MitgliederStammdatenViewModel>();
        public MitgliederUebersichtViewModel MitgliederUebersichtView => ServiceLocator.Current.GetInstance<MitgliederUebersichtViewModel>();
        public MitgliederImportViewModel MitgliederImport => ServiceLocator.Current.GetInstance<MitgliederImportViewModel>();
        public SchluesselStammdatenViewModel SchluesselStammdaten => ServiceLocator.Current.GetInstance<SchluesselStammdatenViewModel>();
        public SchluesselUebersichtViewModel SchluesselUebersicht => ServiceLocator.Current.GetInstance<SchluesselUebersichtViewModel>();
        public SchluesselzuteilungStammdatenViewModel SchluesselzuteilungStammdaten => new SchluesselzuteilungStammdatenViewModel();
        public SchluesselbesitzerAuswahlViewModel SchluesselbesitzerAuswahl => new SchluesselbesitzerAuswahlViewModel();
        public SchluesselbesitzerStammdatenViewModel SchluesselbesitzerStammdaten => ServiceLocator.Current.GetInstance<SchluesselbesitzerStammdatenViewModel>();
        public MitgliedAuswahlViewModel MitgliedAuswahl => new MitgliedAuswahlViewModel();
        public SchluesselbesitzerUebersichtViewModel SchluesselbesitzerUebersicht => ServiceLocator.Current.GetInstance<SchluesselbesitzerUebersichtViewModel>();
        public SchluesselAuswahlViewModel SchluesselAuswahl => ServiceLocator.Current.GetInstance<SchluesselAuswahlViewModel>();
        public SchluesselverteilungBesitzerUebersichtViewModel SchluesselverteilungBesitzerUebersicht => ServiceLocator.Current.GetInstance<SchluesselverteilungBesitzerUebersichtViewModel>();
        public SchluesselverteilungBesitzerUebersichtDetailViewModel SchluesselverteilungBesitzerUebersichtDetail => ServiceLocator.Current.GetInstance<SchluesselverteilungBesitzerUebersichtDetailViewModel>();
        public SchluesselverteilungSchluesselUebersichtViewModel SchluesselverteilungSchluesselUebersicht => ServiceLocator.Current.GetInstance<SchluesselverteilungSchluesselUebersichtViewModel>();
        public SchluesselverteilungSchluesselUebersichtDetailViewModel SchluesselverteilungSchluesselUebersichtDetail => ServiceLocator.Current.GetInstance<SchluesselverteilungSchluesselUebersichtDetailViewModel>();
        public SchluesselverteilungFreieSchluesselUebersichtViewModel SchluesselverteilungFreieSchluesselUebersicht => ServiceLocator.Current.GetInstance<SchluesselverteilungFreieSchluesselUebersichtViewModel>();
        public SchluesselRueckgabeStammdatenViewModel SchluesselRueckgabeStammdaten => ServiceLocator.Current.GetInstance<SchluesselRueckgabeStammdatenViewModel>();
        public SchluesselzuteilungAuswahlViewModel SchluesselzuteilungAuswahl => ServiceLocator.Current.GetInstance<SchluesselzuteilungAuswahlViewModel>();
        public SchluesselzuteilungHistoryUebersichtViewModel SchluesselzuteilungHistoryUebersicht => ServiceLocator.Current.GetInstance<SchluesselzuteilungHistoryUebersichtViewModel>();
        public SchnurschiessenAuszeichnungStammdatenViewModel SchnurschiessenAuszeichnungStammdaten => new SchnurschiessenAuszeichnungStammdatenViewModel();
        public SchnurschiessenAuszeichnungUebersichtViewModel SchnurschiessenAuszeichnungUebersicht => new SchnurschiessenAuszeichnungUebersichtViewModel();
        public SchnurschiessenrangStammdatenViewModel SchnurschiessenrangStammdaten => new SchnurschiessenrangStammdatenViewModel();
        public SchnurschiessenrangUebersichtViewModel SchnurschiessenrangUebersicht =>new SchnurschiessenrangUebersichtViewModel();
        public StartingProgrammViewModel StartingProgramm => new StartingProgrammViewModel();
        public PinAusgabeStammdatenViewModel PinAusgabeStammdaten => new PinAusgabeStammdatenViewModel();
        public PinAusgabeUebersichtViewModel PinAusgabeUebersicht => new PinAusgabeUebersichtViewModel();
        public PinAusgabeMitgliedUebersichtViewModel PinAusgabeMitgliedUebersicht => new PinAusgabeMitgliedUebersichtViewModel();
        public PinAusgabeAuswertungTagViewModel PinAusgabeAuswertungTag => new PinAusgabeAuswertungTagViewModel();
        public PinAusgabeAuswertungTagStundeViewModel PinAusgabeAuswertungTagStunde => new PinAusgabeAuswertungTagStundeViewModel();
        public BackendSettingsViewModel BackendSettings => ServiceLocator.Current.GetInstance<BackendSettingsViewModel>();
        public KonfigruationViewModel Konfigruation => new KonfigruationViewModel();
        public PinAusgabeAuswahlViewModel PinAusgabeAuswahl => new PinAusgabeAuswahlViewModel();
        public LoadingViewModel Loading => new LoadingViewModel();
        public ExportSchluesselViewModel ExportSchluessel => new ExportSchluesselViewModel();
        public ExportMitgliederAenderungenViewModel ExportMitgliederAenderungen => new ExportMitgliederAenderungenViewModel();
        public KkSchiessenUebersichtViewModel KkSchiessenUebersicht => new KkSchiessenUebersichtViewModel();
        public KkSchiessenStammdatenViewModel KkSchiessenStammdaten => new KkSchiessenStammdatenViewModel();
        public KkSchiessgruppeAuswahlViewModel KkSchiessgruppeAuswahl => new KkSchiessgruppeAuswahlViewModel();
        public KkSchiessGruppeStammdatenViewModel KkSchiessGruppeStammdaten => new KkSchiessGruppeStammdatenViewModel();
        public KkSchiessGruppUebersichtViewModel KkSchiessGruppUebersicht => new KkSchiessGruppUebersichtViewModel();
        public KkSchiessenMonatAuswertungViewModel KkSchiessenMonatAuswertung => new KkSchiessenMonatAuswertungViewModel();
        public KkSchiessenMonatJahresvergleichAuswertungViewModel KkSchiessenMonatJahresvergleichAuswertung => new KkSchiessenMonatJahresvergleichAuswertungViewModel();
        public VereinsmeisterschaftAktivViewModel VereinsmeisterschaftAktiveVereinsmeisterschaft => new VereinsmeisterschaftAktivViewModel();
        public VereinsmeisterschaftNeueErstellenViewModel VereinsmeisterschaftNeueErstellen => new VereinsmeisterschaftNeueErstellenViewModel();
        public SchuetzenUebersichtViewModel SchuetzenUebersicht => new SchuetzenUebersichtViewModel();
        public SchuetzeStammdatenViewModel SchuetzeStammdaten => new SchuetzeStammdatenViewModel();
        public SchiessgruppenUebersichtViewModel SchiessgruppeUebersicht => new SchiessgruppenUebersichtViewModel();
        public SchiessgruppeStammdatenViewModel SchiessgruppeStammdaten => new SchiessgruppeStammdatenViewModel();
        public VereinsmeisterschaftNeuerSchuetzeViewModel VereinsmeisterschaftNeuerSchuetze => new VereinsmeisterschaftNeuerSchuetzeViewModel();
        public VereinsmeisterschaftSchuetzeAuswahlViewModel VereinsmeisterschaftSchuetzeAuswahl => new VereinsmeisterschaftSchuetzeAuswahlViewModel();
        public VereinsmeisterschaftFreieGruppeAuswahlViewModel VereinsmeisterschaftFreieGruppeAuswahl => new VereinsmeisterschaftFreieGruppeAuswahlViewModel();
        public VereinsmeisterschaftGruppenMitSchuetzenViewModel VereinsmeisterschaftGruppenMitSchuetzen => new VereinsmeisterschaftGruppenMitSchuetzenViewModel();
        public VereinsmeisterschaftSchuetzenDerGruppeViewModel VereinsmeisterschaftSchuetzenDerGruppe => new VereinsmeisterschaftSchuetzenDerGruppeViewModel();
        public VereinsmeisterschaftGruppenVonSchuetzeViewModel VereinsmeisterschaftGruppenVonSchuetze => new VereinsmeisterschaftGruppenVonSchuetzeViewModel();
        public VereinsmeisterschaftErgebnisEintragenViewModel VereinsmeisterschaftErgebnisEintragen => new VereinsmeisterschaftErgebnisEintragenViewModel();
        public ExportVereinsmeisterschaftViewModel ExportVereinsmeisterschaft => new ExportVereinsmeisterschaftViewModel();
        public BestaetigungViewModel Bestaetigung => new BestaetigungViewModel();
        public VereinsmeisterschaftAuswahlViewModel VereinsmeisterschaftAuswahl => new VereinsmeisterschaftAuswahlViewModel();
        public VereinsmeisterschaftAktivErgebnisseSchuetzentypenViewModel VereinsmeisterschaftAktivErgebnisseSchuetzentypen => new VereinsmeisterschaftAktivErgebnisseSchuetzentypenViewModel();
        public VereinsmeisterschaftAktivErgebnisseVonSchuetzenTypViewModel VereinsmeisterschaftAktivErgebnisseVonSchuetzenTyp => new VereinsmeisterschaftAktivErgebnisseVonSchuetzenTypViewModel();
        public VereinsmeisterschaftAktivErgebnisseGruppentypenViewModel VereinsmeisterschaftAktivErgebnisseGruppentypen => new VereinsmeisterschaftAktivErgebnisseGruppentypenViewModel();
        public VereinsmeisterschaftAktivErgebnisseVonGruppenTypViewModel VereinsmeisterschaftAktivErgebnisseVonGruppenTyp => new VereinsmeisterschaftAktivErgebnisseVonGruppenTypViewModel();
        public VereinsmeisterschaftenUebersichtViewModel VereinsmeisterschaftenUebersicht => new VereinsmeisterschaftenUebersichtViewModel();
        public VereinsmeisterschaftPlatzierungenGruppentypenViewModel VereinsmeisterschaftPlatzierungenGruppentypen => new VereinsmeisterschaftPlatzierungenGruppentypenViewModel();
        public VereinsmeisterschaftPlatzierungenVonGruppentypViewModel VereinsmeisterschaftPlatzierungenVonGruppentyp => new VereinsmeisterschaftPlatzierungenVonGruppentypViewModel();
        public VereinsmeisterschaftPlatzierungenSchuetzentypenViewModel VereinsmeisterschaftPlatzierungenSchuetzentypen => new VereinsmeisterschaftPlatzierungenSchuetzentypenViewModel();
        public VereinsmeisterschaftPlatzierungenVonSchuetzentypViewModel VereinsmeisterschaftPlatzierungenVonSchuetzentyp => new VereinsmeisterschaftPlatzierungenVonSchuetzentypViewModel();
        public VereinsmeisterschaftSchuetzeErgebnisseViewModel VereinsmeisterschaftSchuetzeErgebnisse => new VereinsmeisterschaftSchuetzeErgebnisseViewModel();
        public VereinsmeisterschaftGruppeErgebnisseViewModel VereinsmeisterschaftGruppeErgebnisse => new VereinsmeisterschaftGruppeErgebnisseViewModel();
        public AuswertungVereinsmeisterschaftEntwicklungSchuetzenViewModel AuswertungVereinsmeisterschaftEntwicklungSchuetzen => new AuswertungVereinsmeisterschaftEntwicklungSchuetzenViewModel();
        public AuswertungVereinsmeisterschaftEntwicklungGruppenViewModel AuswertungVereinsmeisterschaftEntwicklungGruppen => new AuswertungVereinsmeisterschaftEntwicklungGruppenViewModel();
        public InfoViewModel Info => new InfoViewModel();
        public LoginViewModel Login => new LoginViewModel();
        public UserStammdatenViewModel UserStammdaten => new UserStammdatenViewModel();
        public UserUebersichtViewModel UserUebersicht => new UserUebersichtViewModel();
        public UserBerechtigungenUebersichtViewModel UserBerechtigungenUebersicht => new UserBerechtigungenUebersichtViewModel();
        public PinsVomMitgliedUebersichtViewModel PinsVomMitgliedUebersicht => new PinsVomMitgliedUebersichtViewModel();
        public MitgliederAuswertungEintrittViewModel MitgliederAuswertungEintritt => new MitgliederAuswertungEintrittViewModel();
        public MitgliederAuswertungJahreImVereinViewModel MitgliederAuswertungJahreImVerein => new MitgliederAuswertungJahreImVereinViewModel();
        public MitgliederAuswertungJahrgangViewModel MitgliederAuswertungJahrgang => new MitgliederAuswertungJahrgangViewModel();
        public MitgliederAuswertungJahrzehnteViewModel MitgliederAuswertungJahrzehnte => new MitgliederAuswertungJahrzehnteViewModel();
        public UserPasswordAendernViewModel UserPasswordAendern => new UserPasswordAendernViewModel();
        public KoenigschiessenUebersichtViewModel KoenigschiessenUebersicht => new KoenigschiessenUebersichtViewModel();
        public KoenigschiessenErstellenViewModel KoenigschiessenErstellen => new KoenigschiessenErstellenViewModel();
        public KoenigschiessenAnmeldungUebersichtViewModel KoenigschiessenAnmeldungUebersicht => new KoenigschiessenAnmeldungUebersichtViewModel();
        public KoenigschiessenAnmeldungBestaetigungViewModel KoenigschiessenAnmeldungBestaetigung => new KoenigschiessenAnmeldungBestaetigungViewModel();
        public JugendkoenigUebersichtViewModel JugendkoenigUebersicht => new JugendkoenigUebersichtViewModel();
        public JugendkoenigschiessenErstellenViewModel JugendkoenigschiessenErstellen => new JugendkoenigschiessenErstellenViewModel();
        public KoenigschiessenAnmeldungWerteKoenigViewModel KoenigschiessenAnmeldungKoenigWerte => new KoenigschiessenAnmeldungWerteKoenigViewModel();
        public KoenigschiessenAnmeldungWerteJugendkoenigViewModel KoenigschiessenAnmeldungWerteJugendkoenig => new KoenigschiessenAnmeldungWerteJugendkoenigViewModel();
        public KoenigschiessenRundeTeilnehmerUebersichtViewModel KoenigschiessenRundeTeilnehmerUebersicht => new KoenigschiessenRundeTeilnehmerUebersichtViewModel();
        public KoenigschiessenErgebnisEintragenViewModel KoenigschiessenErgebnisEintragen => new KoenigschiessenErgebnisEintragenViewModel();
        public KoenigschiessenRundeTeilnehmerWerteViewModel KoenigschiessenRundeTeilnehmerWerte => new KoenigschiessenRundeTeilnehmerWerteViewModel();
        public KoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewModel KoenigschiessenHoechsteErgebnisSchuetzenUebersicht => new KoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewModel();
        public KoenigschiessenRundeAbschlussViewModel KoenigschiessenRundeAbschluss => new KoenigschiessenRundeAbschlussViewModel();
        public KoenigschiessenRundenZahlenViewModel KoenigschiessenRundenZahlen => new KoenigschiessenRundenZahlenViewModel();
        public KoenigschiessenErgebnisseVonMitgliedViewModel KoenigschiessenErgebnisseVonMitglied => new KoenigschiessenErgebnisseVonMitgliedViewModel();
        public SchnurschiessenMitgliederUebersichtViewModel SchnurschiessenMitgliederUebersicht => new SchnurschiessenMitgliederUebersichtViewModel();
        public SchnurschiessenMitgliedHistorieUebersichtViewModel SchnurschiessenMitgliedHistorieUebersicht => new SchnurschiessenMitgliedHistorieUebersichtViewModel();
        public SchnurschiessenAuszeichnungBestandHistorieViewModel SchnurschiessenAuszeichnungBestandHistorie => new SchnurschiessenAuszeichnungBestandHistorieViewModel();
        public SchnurschiessenAuszeichnungBestandUebersichtViewModel SchnurschiessenAuszeichnungBestandUebersicht => new SchnurschiessenAuszeichnungBestandUebersichtViewModel();
        public SchnurschiessenAuszeichnungGekauftEintragenViewModel SchnurschiessenAuszeichnungGekauftEintragen => new SchnurschiessenAuszeichnungGekauftEintragenViewModel();
        public AktiveSchnurschiessenVerwaltungViewModel AktiveSchnurschiessenVerwaltung => new AktiveSchnurschiessenVerwaltungViewModel();
        public SchnurschiessenNeuesErstellenViewModel SchnurschiessenNeuesErstellen => new SchnurschiessenNeuesErstellenViewModel();
        public AktivesSchnurschiessenVerwaltungAusgabeSchnurViewModel AktivesSchnurschiessenVerwaltungAusgabeSchnur => new AktivesSchnurschiessenVerwaltungAusgabeSchnurViewModel();
        public AktivesSchnurschiessenMitgliederUebersichtViewModel AktivesSchnurschiessenMitgliederUebersicht => new AktivesSchnurschiessenMitgliederUebersichtViewModel();
        public AktivesSchnurschiessenTeilnahmeEintragenViewModel AktivesSchnurschiessenTeilnahmeEintragen => new AktivesSchnurschiessenTeilnahmeEintragenViewModel();
        public AktiveSchnurschiessenBestandHistorieViewModel AktiveSchnurschiessenBestandHistorie => new AktiveSchnurschiessenBestandHistorieViewModel();
        public SchnurschiessenMitgliederImportViewModel SchnurschiessenMitgliederImport => new SchnurschiessenMitgliederImportViewModel();
        public SchnurschiessenAuswertungAktuellenStandAuszeichnungViewModel SchnurschiessenAuswertungAktuellenStandAuszeichnung => new SchnurschiessenAuswertungAktuellenStandAuszeichnungViewModel();
        public SchnurschiessenRangAuswahlViewModel SchnurschiessenRangAuswahl => new SchnurschiessenRangAuswahlViewModel();
        public SchnurschiessenAuszeichnungAuswahlViewModel SchnurschiessenAuszeichnungAuswahl => new SchnurschiessenAuszeichnungAuswahlViewModel();
        public SchnurschiessenAuswertungAktuellenStandRangViewModel SchnurschiessenAuswertungAktuellenStandRang => new SchnurschiessenAuswertungAktuellenStandRangViewModel();
        public SchnurschiessenAuswertungEntwicklungAuszeichnungViewModel SchnurschiessenAuswertungEntwicklungAuszeichnung => new SchnurschiessenAuswertungEntwicklungAuszeichnungViewModel();
        public SchnurschiessenAuswertungEntwicklungRangViewModel SchnurschiessenAuswertungEntwicklungRang => new SchnurschiessenAuswertungEntwicklungRangViewModel();
        public SchnurschiessenAuswertungGesamtteilnahmeViewModel SchnurschiessenAuswertungGesamtteilnahme => new SchnurschiessenAuswertungGesamtteilnahmeViewModel();
        public SchnurschiessenAuswertungErhalteneAuszeichnungViewModel SchnurschiessenAuswertungErhalteneAuszeichnung => new SchnurschiessenAuswertungErhalteneAuszeichnungViewModel();
        public SchnurschiessenAuswertungNeuerRangViewModel SchnurschiessenAuswertungNeuerRang => new SchnurschiessenAuswertungNeuerRangViewModel();
        public SchnurschiessenAuswertungTeilnahmeProTagViewModel SchnurschiessenAuswertungTeilnahmeProTag => new SchnurschiessenAuswertungTeilnahmeProTagViewModel();
        public SchnurschiessenMitgliederZuordnungViewModel SchnurschiessenMitgliederZuordnung => new SchnurschiessenMitgliederZuordnungViewModel();
        public ExportSchnurschiessenViewModel ExportSchnurschiessen => new ExportSchnurschiessenViewModel();
        public static void Cleanup()
        {

        }
    }
}