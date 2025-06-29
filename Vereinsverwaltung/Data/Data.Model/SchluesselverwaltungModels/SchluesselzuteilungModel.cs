using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchluesselverwaltungModels
{
    public class SchluesselzuteilungModel
    {
        public int ID { get; set; }
        public int SchluesselID { get; set; }
        public int SchluesselbesitzerID { get; set; }
        public string SchluesselBezeichnung { get; set; }
        public string SchluesselbesitzerName { get; set; }
        public int Anzahl { get; set; }
        public DateTime ErhaltenAm { get; set; }
        public String Kennung { get; set; }

        public SchluesselzuteilungModel()
        {
            this.SchluesselBezeichnung = "";
            this.SchluesselbesitzerName = "";
        }
    }
}
