using Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.UI
{
    public static class BerechtigungenService
    {
        public static IList<BerechtigungTypes> Berechtigungen { set; get; }
        public static bool IsAdmin { get; set; }
        public static bool HatBerechtigung(BerechtigungTypes berechtigung) => Berechtigungen.Any(b => b.Equals(berechtigung));
        
        public static BerechtigungTypes StringToTyp(string berechtigung)
        {
            return berechtigung.ToLower() switch
            {
                "mitgliederimport" => BerechtigungTypes.MitgliederImport,
                "mitgliederstammdatenpflege" => BerechtigungTypes.MitgliederStammdatenPflege,
                "allgemeinmitglieder" => BerechtigungTypes.AllgemeinMitglieder,
                "allgemeinschluessel" => BerechtigungTypes.AllgemeinSchluessel,
                "allgemeinkkschiessen" => BerechtigungTypes.AllgemeinKKSchiessen,
                "allgemeinvereinsmeisterschaft" => BerechtigungTypes.AllgemeinVereinsmeisterschaft,
                "allgemeinauswertung" => BerechtigungTypes.AllgemeinAuswertung,
                "allgemeinexport" => BerechtigungTypes.AllgemeinExport,
                "schluesselstammdatenuebersicht" => BerechtigungTypes.SchluesselStammdatenUebersicht,
                "schluesselstammdatenpflege" => BerechtigungTypes.SchluesselStammdatenPflege,
                "mitgliederuebersicht" => BerechtigungTypes.MitgliederUebersicht,
                "schluesselzuteilung" => BerechtigungTypes.SchluesselZuteilung,
                "schluesselhistorie" => BerechtigungTypes.SchluesselHistorie,
                "pinsausagabeerstellen" => BerechtigungTypes.PinsAusagabeErstellen,
                "pinausgabeueberischt" => BerechtigungTypes.PinAusgabeUeberischt,
                "pinausgabeausfuehren" => BerechtigungTypes.PinAusgabeAusfuehren,
                "kkschiessen" => BerechtigungTypes.KKschiessen,
                "vereinsmeisterschaftstammdatenuebersicht" => BerechtigungTypes.VereinsmeisterschaftStammdatenUebersicht,
                "vereinsmeisterschaftstammdatenpflege" => BerechtigungTypes.VereinsmeisterschaftStammdatenPflege,
                "vereinsmeisterschaftergebnisse" => BerechtigungTypes.VereinsmeisterschaftErgebnisse,
                "vereinsmeisterschaftdurchfuehrung" => BerechtigungTypes.VereinsmeisterschaftDurchfuehrung,
                "auswertungpinausgabe" => BerechtigungTypes.AuswertungPinAusgabe,
                "auswertungkkschiessen" => BerechtigungTypes.AuswertungKKSchiessen,
                "auswertungvereinsmeisterschaft" => BerechtigungTypes.AuswertungVereinsmeisterschaft,
                "exportschluessel" => BerechtigungTypes.ExportSchluessel,
                "exportmitgliedaenderungen" => BerechtigungTypes.ExportMitgliedAenderungen,
                "exportvereinsmeisterschaft" => BerechtigungTypes.ExportVereinsmeisterschaft,
                "optionbenutzerverwaltung" => BerechtigungTypes.OptionBenutzerverwaltung,
                "optionbackendsettings" => BerechtigungTypes.OptionBackendSettings,
                "allgemeinpins" => BerechtigungTypes.AllgemeinPins,
                "optionschnurrschiessen" => BerechtigungTypes.OptionSchnurrschiessen,
                "auswertungmitglieder" => BerechtigungTypes.AuswertungMitglieder,
                _ => BerechtigungTypes.Undefiniert,
            };
        }
    }
}
