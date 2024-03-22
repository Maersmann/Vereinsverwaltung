using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class AktivesSchnurschiessenMitgliederUebersichtModel
    {
        public int MitgliedID { get; set; }
        public string Name { get; set; }
        public string Rang { get; set; }
        public int? LetztesJahr { get; set; }
        public string NaechsteRang { get; set; }
        public bool HatTeilgenommen {  get; set; }
        public DateTime? TeilnahmeAm {  get; set; }
        public bool HatSchnurMitgenommen { get; set; }
        public bool KannTeilnehemen {  get; set; }
        public DateTime? Geburtstag { get; set; }
        public string Straße { get; set; }
    }
}
