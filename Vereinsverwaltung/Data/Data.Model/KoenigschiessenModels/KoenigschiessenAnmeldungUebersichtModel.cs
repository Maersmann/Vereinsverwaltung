using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class KoenigschiessenAnmeldungUebersichtModel
    {
        public int ID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public KoenigschiessenArt Art { get; set; }
        public int MitgliedID { get; set; }
        public int Mitgliedsnr { get; set; }
        public string Strasse { get; set; }
        public DateTime Eintrittsdatum { get; set; }
        public DateTime Geburtstag { get; set; }
        public DateTime? AngemeldetAm { get; set; }
        public bool Angemeldet { get; set; }
        public string Ort { get; set; }
        public int? Alter { get; set; }

        public bool NichtAngemeldet => !Angemeldet;
        public string Fullname => Vorname + " " + Nachname;
    }
}
