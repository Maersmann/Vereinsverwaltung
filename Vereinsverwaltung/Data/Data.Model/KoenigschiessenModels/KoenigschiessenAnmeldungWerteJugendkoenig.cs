using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class KoenigschiessenAnmeldungWerteJugendkoenig
    {
        public int JugendkoenigOffen { get; set; }
        public int JugendkoenigTeilgenommen { get; set; }
        public int JugendkoenigGesamt => JugendkoenigOffen + JugendkoenigTeilgenommen;

        public int JugendkoeniginOffen { get; set; }
        public int JugendkoeniginTeilgenommen { get; set; }
        public int JugendkoeniginGesamt => JugendkoeniginOffen + JugendkoeniginTeilgenommen;

        public int Offen => JugendkoeniginOffen + JugendkoenigOffen;
        public int Teilgenommen => JugendkoeniginTeilgenommen + JugendkoenigTeilgenommen;
        public int Gesamt => Offen + Teilgenommen;

        public DateTime? JugendkoenigBis{ get; set; }
        public int? JugendkoenigBisAlter { get; set; }
        public DateTime? JugendkoeniginBis { get; set; }
        public int? JugendkoeniginBisAlter { get; set; }
    }
}
