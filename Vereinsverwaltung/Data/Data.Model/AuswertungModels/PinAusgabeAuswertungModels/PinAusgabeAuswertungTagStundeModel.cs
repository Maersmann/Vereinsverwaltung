using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.AuswertungModels.PinAusgabeAuswertungModels
{
    public class PinAusgabeAuswertungTagStundeModel
    {
        public string Bezeichnung { get; set; }
        public string PinBezeichnung { get; set; }
        public bool Abgeschlossen { get; set; }
        public int Verteilt { get; set; }
        public int Offen { get; set; }
        public IList<AuswertungTagStundeAnzahlModel> Auswertung { get; set; }

        public PinAusgabeAuswertungTagStundeModel()
        {
            Auswertung = new List<AuswertungTagStundeAnzahlModel>();
            Bezeichnung = "";
        }
        
    }
}
