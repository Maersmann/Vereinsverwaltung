using Data.Types.SchluesselverwaltungTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchluesselverwaltungModels
{
    public class SchluesselzuteilungHistoryModel
    {
        public int ID { get; set; }
        public DateTime Datum { get; set; }
        public int Anzahl { get; set; }
        public ZuteilungState State { get; set; }
        public string SchluesselBezeichnung { get; set; }
        public string SchluesselbesitzerName { get; set; }
    }
}
