using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.AuswahlModels
{
    public class SchluesselzuteilungAuswahlModel
    {
        public int ID { get; set; }
        public int Anzahl{get;set;}
        public DateTime ErhaltenAm { get; set; }
        public String SchluesselbesitzerName { get; set; }
        public String SchluesselBezeichnung { get; set; }
    }
}
