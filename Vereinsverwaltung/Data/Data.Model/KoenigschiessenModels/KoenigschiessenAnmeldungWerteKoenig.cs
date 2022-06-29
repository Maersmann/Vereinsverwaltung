using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class KoenigschiessenAnmeldungWerteKoenig
    {
        public int KoenigOffen { get; set; }
        public int KoenigTeilgenommen { get; set; }
        public int KoenigGesamt => KoenigOffen + KoenigTeilgenommen;

        public int VizeKoenigOffen { get; set; }
        public int VizeKoenigTeilgenommen { get; set; }
        public int VizeKoenigGesamt => VizeKoenigOffen + VizeKoenigTeilgenommen;

        public int BesteSchuetzinOffen { get; set; }
        public int BesteSchuetzinTeilgenommen { get; set; }
        public int BesteSchuetzinGesamt => BesteSchuetzinOffen + BesteSchuetzinTeilgenommen;

        public int OhneWertungOffen { get; set; }
        public int OhneWertungTeilgenommen { get; set; }
        public int OhneWertungGesamt => OhneWertungTeilgenommen + OhneWertungOffen;

        public int Offen => VizeKoenigOffen + KoenigOffen + BesteSchuetzinOffen + OhneWertungOffen;
        public int Teilgenommen => VizeKoenigTeilgenommen + KoenigTeilgenommen + BesteSchuetzinTeilgenommen + OhneWertungTeilgenommen;
        public int Gesamt => Offen + Teilgenommen;

        public DateTime? KoenigVon { get; set; }
        public int? KoenigVonAlter { get; set; }
        public DateTime? KoenigBis { get; set; }
        public int? KoenigBisAlter { get; set; }

        public DateTime? VizeKoenigVon { get; set; }
        public int? VizeKoenigVonAlter { get; set; }
        public DateTime? VizeKoenigBis { get; set; }
        public int? VizeKoenigBisAlter { get; set; }

        public DateTime? BesteSchuetzinVon { get; set; }
        public int? BesteSchuetzinVonAlter { get; set; }


        public KoenigschiessenAnmeldungWerteKoenig()
        {
            KoenigOffen = 0;
            KoenigTeilgenommen = 0;
            VizeKoenigOffen = 0;
            VizeKoenigTeilgenommen = 0;
            BesteSchuetzinOffen = 0;
            BesteSchuetzinTeilgenommen = 0;
            OhneWertungOffen = 0;
            OhneWertungTeilgenommen = 0;
        }

    }
}
