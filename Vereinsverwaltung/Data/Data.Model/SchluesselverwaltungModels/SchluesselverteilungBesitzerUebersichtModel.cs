using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchluesselverwaltungModels
{
    public class SchluesselverteilungBesitzerUebersichtModel
    {
        public int Anzahl { get; set; }
        public DateTime ErhaltenAm { get; set; }
        public string Name { get; set; }
        public int SchluesselbesitzerID { get; set; }
        public bool DokumentationRueckgabeErstellt { get; set; }
        public bool DokumentationRueckgabeAbgeschlossen { get; set; }
        public bool DokumentationZuteilungErstellt { get; set; }
        public bool DokumentationZuteilungAbgeschlossen { get; set; }
        public bool DokumentationErstellt { get => DokumentationZuteilungErstellt && DokumentationRueckgabeErstellt; }
        public bool DokumentationAbgeschlossen { get => DokumentationZuteilungAbgeschlossen && DokumentationRueckgabeAbgeschlossen; }
        public bool CanDokumentation
        {
            get
            {
                return !DokumentationErstellt || !DokumentationAbgeschlossen;
            }
        }
    }
}
