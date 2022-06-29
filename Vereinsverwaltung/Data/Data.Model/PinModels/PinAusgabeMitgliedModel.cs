using Data.Model.MitgliederModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.PinModels
{
    public class PinAusgabeMitgliedModel
    {
        public int ID { get; set; }
        public bool Erhalten { get; set; }
        public int PinAusgabeID { get; set; }
        public string Beschreibung { get; set; }
        public DateTime? ErhaltenAm { get; set; }
        public MitgliederModel Mitglied { get; set; }
        public PinAusgabeModel PinAusgabe { get; set; }

        public bool NichtErhalten => !Erhalten && !PinAusgabe.Abgeschlossen;
    }
}
