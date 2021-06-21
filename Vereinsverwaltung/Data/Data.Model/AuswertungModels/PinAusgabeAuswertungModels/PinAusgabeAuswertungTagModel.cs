using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.AuswertungModels.PinAusgabeAuswertungModels
{
    public class PinAusgabeAuswertungTagModel
    {
        public string Bezeichnung { get; set; }
        public string PinBezeichnung { get; set; }
        public bool Abgeschlossen { get; set; }
        public int Verteilt { get; set; }
        public int Offen { get; set; }
        public IList<AuswertungTagAnzahlModel> Auswertung { get; set; }

        public PinAusgabeAuswertungTagModel()
        {
            Auswertung = new List<AuswertungTagAnzahlModel>();
            Bezeichnung = "";
        }
        
    }
}
