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
using CommunityToolkit.Mvvm.Messaging;
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
using Logic.UI.SchuetzenfestViewModels;
using Logic.UI.AuswertungenViewModels.SchuetzenfestAuswertungenViewModels;
using System.Diagnostics;

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
            //ServiceLocator.SetLocatorProvider(() => ServiceLocator.Current);
        }

        public static MainViewModel Main => new();
        public static MitgliederStammdatenViewModel MitgliederStammdaten => new();
        public static MitgliederUebersichtViewModel MitgliederUebersichtView => new();
        public static MitgliederImportViewModel MitgliederImport => new();
        public static SchluesselStammdatenViewModel SchluesselStammdaten => new();
        public static SchluesselUebersichtViewModel SchluesselUebersicht => new();
        public static SchluesselzuteilungStammdatenViewModel SchluesselzuteilungStammdaten => new();
        public static  SchluesselbesitzerAuswahlViewModel SchluesselbesitzerAuswahl => new();
        public static SchluesselbesitzerStammdatenViewModel SchluesselbesitzerStammdaten => new();
        public static MitgliedAuswahlViewModel MitgliedAuswahl => new();
        public static SchluesselbesitzerUebersichtViewModel SchluesselbesitzerUebersicht => new();
        public static SchluesselAuswahlViewModel SchluesselAuswahl => new();
        public static SchluesselverteilungBesitzerUebersichtViewModel SchluesselverteilungBesitzerUebersicht => new ();
        public static SchluesselverteilungBesitzerUebersichtDetailViewModel SchluesselverteilungBesitzerUebersichtDetail => new();
        public static SchluesselverteilungSchluesselUebersichtViewModel SchluesselverteilungSchluesselUebersicht => new();
        public static SchluesselverteilungSchluesselUebersichtDetailViewModel SchluesselverteilungSchluesselUebersichtDetail => new();
        public static SchluesselverteilungFreieSchluesselUebersichtViewModel SchluesselverteilungFreieSchluesselUebersicht => new();
        public static SchluesselRueckgabeStammdatenViewModel SchluesselRueckgabeStammdaten => new();
        public static SchluesselzuteilungAuswahlViewModel SchluesselzuteilungAuswahl => new();
        public static SchluesselzuteilungHistoryUebersichtViewModel SchluesselzuteilungHistoryUebersicht => new();
        public static SchnurschiessenAuszeichnungStammdatenViewModel SchnurschiessenAuszeichnungStammdaten => new();
        public static SchnurschiessenAuszeichnungUebersichtViewModel SchnurschiessenAuszeichnungUebersicht => new();
        public static SchnurschiessenrangStammdatenViewModel SchnurschiessenrangStammdaten => new();
        public static SchnurschiessenrangUebersichtViewModel SchnurschiessenrangUebersicht =>new();
        public static StartingProgrammViewModel StartingProgramm => new ();
        public static PinAusgabeStammdatenViewModel PinAusgabeStammdaten => new ();
        public static PinAusgabeUebersichtViewModel PinAusgabeUebersicht => new ();
        public static PinAusgabeMitgliedUebersichtViewModel PinAusgabeMitgliedUebersicht => new ();
        public static PinAusgabeAuswertungTagViewModel PinAusgabeAuswertungTag => new ();
        public static PinAusgabeAuswertungTagStundeViewModel PinAusgabeAuswertungTagStunde => new ();
        public static BackendSettingsViewModel BackendSettings => new();
        public static KonfigruationViewModel Konfigruation => new ();
        public static PinAusgabeAuswahlViewModel PinAusgabeAuswahl => new ();
        public static LoadingViewModel Loading => new ();
        public static ExportSchluesselViewModel ExportSchluessel => new ();
        public static ExportMitgliederAenderungenViewModel ExportMitgliederAenderungen => new ();
        public static KkSchiessenUebersichtViewModel KkSchiessenUebersicht => new ();
        public static KkSchiessenStammdatenViewModel KkSchiessenStammdaten => new ();
        public static KkSchiessgruppeAuswahlViewModel KkSchiessgruppeAuswahl => new ();
        public static KkSchiessGruppeStammdatenViewModel KkSchiessGruppeStammdaten => new ();
        public static KkSchiessGruppUebersichtViewModel KkSchiessGruppUebersicht => new ();
        public static KkSchiessenMonatAuswertungViewModel KkSchiessenMonatAuswertung => new ();
        public static KkSchiessenMonatJahresvergleichAuswertungViewModel KkSchiessenMonatJahresvergleichAuswertung => new ();
        public static VereinsmeisterschaftAktivViewModel VereinsmeisterschaftAktiveVereinsmeisterschaft => new ();
        public static VereinsmeisterschaftNeueErstellenViewModel VereinsmeisterschaftNeueErstellen => new ();
        public static SchuetzenUebersichtViewModel SchuetzenUebersicht => new ();
        public static SchuetzeStammdatenViewModel SchuetzeStammdaten => new ();
        public static SchiessgruppenUebersichtViewModel SchiessgruppeUebersicht => new ();
        public static SchiessgruppeStammdatenViewModel SchiessgruppeStammdaten => new ();
        public static VereinsmeisterschaftNeuerSchuetzeViewModel VereinsmeisterschaftNeuerSchuetze => new ();
        public static VereinsmeisterschaftSchuetzeAuswahlViewModel VereinsmeisterschaftSchuetzeAuswahl => new ();
        public static VereinsmeisterschaftFreieGruppeAuswahlViewModel VereinsmeisterschaftFreieGruppeAuswahl => new ();
        public static VereinsmeisterschaftGruppenMitSchuetzenViewModel VereinsmeisterschaftGruppenMitSchuetzen => new ();
        public static VereinsmeisterschaftSchuetzenDerGruppeViewModel VereinsmeisterschaftSchuetzenDerGruppe => new ();
        public static VereinsmeisterschaftGruppenVonSchuetzeViewModel VereinsmeisterschaftGruppenVonSchuetze => new ();
        public static VereinsmeisterschaftErgebnisEintragenViewModel VereinsmeisterschaftErgebnisEintragen => new ();
        public static ExportVereinsmeisterschaftViewModel ExportVereinsmeisterschaft => new ();
        public static BestaetigungViewModel Bestaetigung => new ();
        public static VereinsmeisterschaftAuswahlViewModel VereinsmeisterschaftAuswahl => new ();
        public static VereinsmeisterschaftAktivErgebnisseSchuetzentypenViewModel VereinsmeisterschaftAktivErgebnisseSchuetzentypen => new ();
        public static VereinsmeisterschaftAktivErgebnisseVonSchuetzenTypViewModel VereinsmeisterschaftAktivErgebnisseVonSchuetzenTyp => new ();
        public static VereinsmeisterschaftAktivErgebnisseGruppentypenViewModel VereinsmeisterschaftAktivErgebnisseGruppentypen => new ();
        public static VereinsmeisterschaftAktivErgebnisseVonGruppenTypViewModel VereinsmeisterschaftAktivErgebnisseVonGruppenTyp => new ();
        public static VereinsmeisterschaftenUebersichtViewModel VereinsmeisterschaftenUebersicht => new ();
        public static VereinsmeisterschaftPlatzierungenGruppentypenViewModel VereinsmeisterschaftPlatzierungenGruppentypen => new ();
        public static VereinsmeisterschaftPlatzierungenVonGruppentypViewModel VereinsmeisterschaftPlatzierungenVonGruppentyp => new ();
        public static VereinsmeisterschaftPlatzierungenSchuetzentypenViewModel VereinsmeisterschaftPlatzierungenSchuetzentypen => new ();
        public static VereinsmeisterschaftPlatzierungenVonSchuetzentypViewModel VereinsmeisterschaftPlatzierungenVonSchuetzentyp => new ();
        public static VereinsmeisterschaftSchuetzeErgebnisseViewModel VereinsmeisterschaftSchuetzeErgebnisse => new ();
        public static VereinsmeisterschaftGruppeErgebnisseViewModel VereinsmeisterschaftGruppeErgebnisse => new ();
        public static AuswertungVereinsmeisterschaftEntwicklungSchuetzenViewModel AuswertungVereinsmeisterschaftEntwicklungSchuetzen => new ();
        public static AuswertungVereinsmeisterschaftEntwicklungGruppenViewModel AuswertungVereinsmeisterschaftEntwicklungGruppen => new ();
        public static InfoViewModel Info => new ();
        public static LoginViewModel Login => new ();
        public static UserStammdatenViewModel UserStammdaten => new ();
        public static UserUebersichtViewModel UserUebersicht => new ();
        public static UserBerechtigungenUebersichtViewModel UserBerechtigungenUebersicht => new ();
        public static PinsVomMitgliedUebersichtViewModel PinsVomMitgliedUebersicht => new ();
        public static MitgliederAuswertungEintrittViewModel MitgliederAuswertungEintritt => new ();
        public static MitgliederAuswertungJahreImVereinViewModel MitgliederAuswertungJahreImVerein => new ();
        public static MitgliederAuswertungJahrgangViewModel MitgliederAuswertungJahrgang => new ();
        public static MitgliederAuswertungJahrzehnteViewModel MitgliederAuswertungJahrzehnte => new ();
        public static UserPasswordAendernViewModel UserPasswordAendern => new ();
        public static KoenigschiessenUebersichtViewModel KoenigschiessenUebersicht => new ();
        public static KoenigschiessenErstellenViewModel KoenigschiessenErstellen => new ();
        public static KoenigschiessenAnmeldungUebersichtViewModel KoenigschiessenAnmeldungUebersicht => new ();
        public static KoenigschiessenAnmeldungBestaetigungViewModel KoenigschiessenAnmeldungBestaetigung => new ();
        public static JugendkoenigUebersichtViewModel JugendkoenigUebersicht => new ();
        public static JugendkoenigschiessenErstellenViewModel JugendkoenigschiessenErstellen => new ();
        public static KoenigschiessenAnmeldungWerteKoenigViewModel KoenigschiessenAnmeldungKoenigWerte => new ();
        public static KoenigschiessenAnmeldungWerteJugendkoenigViewModel KoenigschiessenAnmeldungWerteJugendkoenig => new ();
        public static KoenigschiessenRundeTeilnehmerUebersichtViewModel KoenigschiessenRundeTeilnehmerUebersicht => new ();
        public static KoenigschiessenErgebnisEintragenViewModel KoenigschiessenErgebnisEintragen => new ();
        public static KoenigschiessenRundeTeilnehmerWerteViewModel KoenigschiessenRundeTeilnehmerWerte => new ();
        public static KoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewModel KoenigschiessenHoechsteErgebnisSchuetzenUebersicht => new ();
        public static KoenigschiessenRundeAbschlussViewModel KoenigschiessenRundeAbschluss => new ();
        public static KoenigschiessenRundenZahlenViewModel KoenigschiessenRundenZahlen => new ();
        public static KoenigschiessenErgebnisseVonMitgliedViewModel KoenigschiessenErgebnisseVonMitglied => new ();
        public static SchnurschiessenMitgliederUebersichtViewModel SchnurschiessenMitgliederUebersicht => new ();
        public static SchnurschiessenMitgliedHistorieUebersichtViewModel SchnurschiessenMitgliedHistorieUebersicht => new ();
        public static SchnurschiessenAuszeichnungBestandHistorieViewModel SchnurschiessenAuszeichnungBestandHistorie => new ();
        public static SchnurschiessenAuszeichnungBestandUebersichtViewModel SchnurschiessenAuszeichnungBestandUebersicht => new ();
        public static SchnurschiessenAuszeichnungGekauftEintragenViewModel SchnurschiessenAuszeichnungGekauftEintragen => new ();
        public static AktiveSchnurschiessenVerwaltungViewModel AktiveSchnurschiessenVerwaltung => new ();
        public static SchnurschiessenNeuesErstellenViewModel SchnurschiessenNeuesErstellen => new ();
        public static AktivesSchnurschiessenVerwaltungAusgabeSchnurViewModel AktivesSchnurschiessenVerwaltungAusgabeSchnur => new ();
        public static AktivesSchnurschiessenMitgliederUebersichtViewModel AktivesSchnurschiessenMitgliederUebersicht => new ();
        public static AktivesSchnurschiessenTeilnahmeEintragenViewModel AktivesSchnurschiessenTeilnahmeEintragen => new ();
        public static AktiveSchnurschiessenBestandHistorieViewModel AktiveSchnurschiessenBestandHistorie => new ();
        public static SchnurschiessenMitgliederImportViewModel SchnurschiessenMitgliederImport => new ();
        public static SchnurschiessenAuswertungAktuellenStandAuszeichnungViewModel SchnurschiessenAuswertungAktuellenStandAuszeichnung => new ();
        public static SchnurschiessenRangAuswahlViewModel SchnurschiessenRangAuswahl => new ();
        public static SchnurschiessenAuszeichnungAuswahlViewModel SchnurschiessenAuszeichnungAuswahl => new ();
        public static SchnurschiessenAuswertungAktuellenStandRangViewModel SchnurschiessenAuswertungAktuellenStandRang => new ();
        public static SchnurschiessenAuswertungEntwicklungAuszeichnungViewModel SchnurschiessenAuswertungEntwicklungAuszeichnung => new ();
        public static SchnurschiessenAuswertungEntwicklungRangViewModel SchnurschiessenAuswertungEntwicklungRang => new ();
        public static SchnurschiessenAuswertungGesamtteilnahmeViewModel SchnurschiessenAuswertungGesamtteilnahme => new ();
        public static SchnurschiessenAuswertungErhalteneAuszeichnungViewModel SchnurschiessenAuswertungErhalteneAuszeichnung => new ();
        public static SchnurschiessenAuswertungNeuerRangViewModel SchnurschiessenAuswertungNeuerRang => new ();
        public static SchnurschiessenAuswertungTeilnahmeProTagViewModel SchnurschiessenAuswertungTeilnahmeProTag => new ();
        public static SchnurschiessenMitgliederZuordnungViewModel SchnurschiessenMitgliederZuordnung => new ();
        public static ExportSchnurschiessenViewModel ExportSchnurschiessen => new ();
        public static MitgliederAnonymisierenViewModel MitgliederAnonymisieren => new();
        public static SchuetzenfestZahlenStammdatenViewModel SchuetzenfestZahlenStammdaten => new();
        public static SchuetzenfestZahlenUebersichtViewModel SchuetzenfestZahlenUebersicht => new();
        public static SchuetzenfestZahlenAuswertungUmzugViewModel SchuetzenfestZahlenAuswertungUmzug => new();
        public static SchuetzenfestZahlenAuswertungBaendchenViewModel SchuetzenfestZahlenAuswertungBaendchen => new();
        public static SchluesselverteilungDokumentationViewModel SchluesselverteilungDokumentation => new();
        public static SchluesselverteilungKennungEintragenViewModel SchluesselverteilungKennungEintragen => new();
        public static void OnActivated()
        {

        }
    }
}