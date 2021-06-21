using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.PinModels
{
    public class PinAusgabenUebersichtModel
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public PinModel Pin {get;set;}
        public int Gesamt { get; set; }
        public int Verteilt { get; set; }
        public int Offen { get; set; }
        public bool Abgeschlossen { get; set; }
        public bool IsNichtAbgeschlossen => !Abgeschlossen;
    }
}
