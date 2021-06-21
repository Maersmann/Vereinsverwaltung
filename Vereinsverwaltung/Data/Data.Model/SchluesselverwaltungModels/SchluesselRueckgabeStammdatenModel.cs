using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchluesselverwaltungModels
{
    public class SchluesselRueckgabeStammdatenModel
    {
        public string Schluesselbezeichnung { get; set; }
        public string SchluesselbesitzerName { get; set; }
        public int Anzahl { get; set; }
        public DateTime RueckgabeAm { get; set; }
        public int SchluesselzuteilungID { get; set; }
    }
}
