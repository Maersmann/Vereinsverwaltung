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
    }
}
