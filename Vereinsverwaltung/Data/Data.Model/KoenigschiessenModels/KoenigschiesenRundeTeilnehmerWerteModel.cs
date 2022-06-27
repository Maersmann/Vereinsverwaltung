using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class KoenigschiesenRundeTeilnehmerWerteModel
    {
        public int AnzahlOffen { get; set; }
        public int AnzahlAbgegeben { get; set; }
        public int AnzahlSchuetzen => AnzahlOffen + AnzahlAbgegeben;

        public int AnzahlHoechstesErgebnis { get; set; }
        public int HoechstesErgebnis { get; set; }

        public string RundeBezeichnug { get; set; }
    }
}
