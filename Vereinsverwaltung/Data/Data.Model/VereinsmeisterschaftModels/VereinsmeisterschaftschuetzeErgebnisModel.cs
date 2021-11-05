using Data.Types.MitgliederTypes;
using Data.Types.VereinsmeisterschaftTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.VereinsmeisterschaftModels
{
    public class VereinsmeisterschaftschuetzeErgebnisModel
    {
        public int ID { get; set; }
        public int VereinsmeisterschaftID { get; set; }
        public int SchuetzeID { get; set; }
        public int? SchiessgruppeID { get; set; }
        public double Ergebnis { get; set; }
        public int? Platzierung { get; set; }
        public int? Alter { get; set; }
        public bool ErgebnisAbgegeben { get; set; }
        public DateTime? ErgebnisAbgegebenAm { get; set; }
        public DateTime? AngemeldetAm { get; set; }
        public VereinsmeisterschaftSchuetzeTyp VereinsmeisterschaftSchuetzeTyp { get; set; }
        public virtual VereinsmeisterschaftModel Vereinsmeisterschaft { get; set; }
        public virtual SchuetzeModel Schuetze { get; set; }
        public virtual SchiessgruppeModel Schiessgruppe { get; set; }

        public string SchuetzenName { get; set; }
        public string Gruppenname { get; set; }
        public Geschlecht? Geschlecht { get; set; }
    }
}
