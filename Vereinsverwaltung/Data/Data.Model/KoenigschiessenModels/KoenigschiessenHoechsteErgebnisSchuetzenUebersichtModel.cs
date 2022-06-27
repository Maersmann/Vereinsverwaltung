using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class KoenigschiessenHoechsteErgebnisSchuetzenUebersichtModel
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Strasse { get; set; }
        public string Ort { get; set; }
        public int MitgliederNr { get; set; }
        public DateTime Geburtstag { get; set; }
        public DateTime ErgebnisVon { get; set; }
    }
}
