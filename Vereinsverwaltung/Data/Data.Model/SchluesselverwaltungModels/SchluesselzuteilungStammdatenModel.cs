using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchluesselverwaltungModels
{
    public class SchluesselzuteilungStammdatenModel
    {
        public string SchluesselBezeichnung { get; set; }
        public string SchluesselbesitzerName { get; set; }
        public int Anzahl { get; set; }
        public DateTime ErhaltenAm { get; set; }
    }
}
