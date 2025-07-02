using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;

namespace Data.Model.SchluesselverwaltungModels
{
    public class SchluesselbesitzerModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? MitgliedsNr { get; set; }
        public int? MitgliedID { get; set; }
        public bool CanRueckgabe { get; set; }
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
