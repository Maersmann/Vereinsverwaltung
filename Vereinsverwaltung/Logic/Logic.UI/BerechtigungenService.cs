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
        public static bool HatBerechtigung(BerechtigungTypes berechtigung) => Berechtigungen.Any(b => b.Equals(berechtigung));
        
        public static BerechtigungTypes StringToTyp(string berechtigung)
        {
            return berechtigung.ToLower() switch
            {
                "mitgliederimport" => BerechtigungTypes.MitgliederImport,
                "schluessel" => BerechtigungTypes.Schluesselverwaltung,
                _ => BerechtigungTypes.Undefiniert,
            };
        }
    }
}
