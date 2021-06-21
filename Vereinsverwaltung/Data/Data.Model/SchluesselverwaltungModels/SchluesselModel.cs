using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchluesselverwaltungModels
{
    public class SchluesselModel
    {
        public int ID { get; set; }
        public int Nummer { get; set; }
        public string Beschreibung { get; set; }
        public string Bezeichnung { get; set; }
        public int Bestand { get; set; }
        public int Ausgegeben { get; set; }
        public bool CanRueckgabe { get; set; }
        public int FreieAnzahl => Bestand - Ausgegeben;
        public bool CanZuteilen => FreieAnzahl > 0;
    }
}
