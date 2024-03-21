using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum BerechtigungTypes
    {
        Undefiniert = -1,
        [Description("Allgemein - Mitglieder")]
        AllgemeinMitglieder = 0,
        [Description("Allgemein - Schlüsselverwaltung")]
        AllgemeinSchluessel = 1,
        [Description("Allgemein - KK-Schießen")]
        AllgemeinKKSchiessen = 2,
        [Description("Allgemein - Vereinsmeisterschaft")]
        AllgemeinVereinsmeisterschaft = 3,
        [Description("Allgemein - Auswertung")]
        AllgemeinAuswertung = 4,
        [Description("Allgemein - Export")]
        AllgemeinExport = 5,
        [Description("Allgemein - Pins")]
        AllgemeinPins = 30,
        [Description("Allgemein - Schützenfest")]
        SchuetzenfestAllgemein = 32,
        [Description("Mitglieder - Import")]
        MitgliederImport = 6,
        [Description("Mitglieder - Uebersicht")]
        MitgliederUebersicht = 8,
        [Description("Mitglieder - Stammdatenpflege")]
        MitgliederStammdatenPflege = 9,
        [Description("Schlüsselverwaltung - Stammdatenübersicht")]
        SchluesselStammdatenUebersicht = 10,
        [Description("Schlüsselverwaltung - Stammdatenpflege")]
        SchluesselStammdatenPflege = 7,
        [Description("Schlüsselverwaltung - Zuteilung")]
        SchluesselZuteilung = 11,
        [Description("Schlüsselverwaltung - Historie")]
        SchluesselHistorie = 12,
        [Description("Pins - Neue Ausgabe")]
        PinsAusagabeErstellen = 13,
        [Description("Pins - Ausgabeübersicht")]
        PinAusgabeUeberischt = 14,
        [Description("Pins - Ausgeben")]
        PinAusgabeAusfuehren = 15,
        [Description("KKSchießen - Komplett")]
        KKschiessen = 16,
        [Description("Vereinsmeisterschaft - Stammdatenübersicht")]
        VereinsmeisterschaftStammdatenUebersicht = 17,
        [Description("Vereinsmeisterschaft - Stammdatenpflege")]
        VereinsmeisterschaftStammdatenPflege = 18,
        [Description("Vereinsmeisterschaft - Ergebnisse")]
        VereinsmeisterschaftErgebnisse = 19,
        [Description("Vereinsmeisterschaft - Durchführung")]
        VereinsmeisterschaftDurchfuehrung = 20,
        [Description("Auswertung - Pins")]
        AuswertungPinAusgabe = 21,
        [Description("Auswertung - Kk-Schießen")]
        AuswertungKKSchiessen = 22,
        [Description("Auswertung - Vereinsmeisterschaft")]
        AuswertungVereinsmeisterschaft = 23,
        [Description("Auswertung - Mitglieder")]
        AuswertungMitglieder = 31,
        [Description("Export - Schlüssel")]
        ExportSchluessel = 24,
        [Description("Export - Mitgliederänderungen")]
        ExportMitgliedAenderungen = 25,
        [Description("Export - Vereinsmeisterschaft")]
        ExportVereinsmeisterschaft = 26,
        [Description("Optionen - Benutzerverwaltung")]
        OptionBenutzerverwaltung = 27,
        [Description("Optionen - Backend-Settings")]
        OptionBackendSettings = 28,
        [Description("Optionen - Schnurrschießen")]
        OptionSchnurrschiessen = 29,
        [Description("Jugenkönigschiessen Erstellen")]
        JugenkoenigschiessenErstellen = 33,
        [Description("Jugenkönigschiessen Übersicht")]
        JugendkoenigschiessenUebersicht = 34,
        [Description("Königschiessen Erstellen")]
        KoenigschiessenErstellen = 35,
        [Description("Königschiessen Übersicht")]
        KoenigschiessenUebersicht = 36,
        [Description("Königschiessen Ergebnisse Eintragen")]
        KoenigschiessenRundeErgebnisseEintragen = 37 ,
        [Description("Schnurschießen - Allgemein")]
        SchnurschiessenAllgemein = 38,
        [Description("Schnurschießen - Verwaltung")]
        SchnurschiessenVerwaltung = 39,
        [Description("Mitglieder Anonymisieren")]
        MitgliederAnonymisieren = 40,
        [Description("Schützenfestzahlen")]
        Schuetzenfestzahlenuebersicht = 41
    }
}
