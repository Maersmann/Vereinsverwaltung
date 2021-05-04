using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchluesselverwaltungModels
{
    public class SchluesselStammdatenModel
    {
        public int Nummer { get; set; }
        public string Beschreibung { get; set; }
        public string Bezeichnung { get; set; }
        public int Bestand { get; set; }
    }
}
