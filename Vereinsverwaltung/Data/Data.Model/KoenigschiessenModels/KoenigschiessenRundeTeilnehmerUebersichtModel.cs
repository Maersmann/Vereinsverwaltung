using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class KoenigschiessenRundeTeilnehmerUebersichtModel
    {
        public int KoenigschiessenRundeSchuetzeID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public KoenigschiessenArt Art { get; set; }
        public int MitgliedID { get; set; }
        public int Mitgliedsnr { get; set; }
        public string Strasse { get; set; }
        public string Ort { get; set; }
        public DateTime? ErgebnissVom { get; set; }
        public bool ErgebnisAbgegeben { get; set; }
        public int? Ergebnis { get; set; }

        public bool ErgebnisNichtAbgegeben => !ErgebnisAbgegeben;
        public string Fullname => Vorname + " " + Nachname;
    }
}
