using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels.DTOs
{
    public class KoenigschiessenAbschlussDTO
    {
        public bool KoenigschiessenBeendet { get; set; }
        public int HoechstesErgebnis { get; set; }
        public int AnzahlHoechstesErgebnis { get; set; }
        public int SchuetzenTeilgenommen { get; set; }
        public int SchuetzenNichtTeilgenommen { get; set; }
        public string Koenig { get; set; }
        public string RundeBezeichnung { get; set; }
    }
}
