using Data.Types.SchnurschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class SchnurschiessenAuszeichnungBestandUebersichtModel
    {
        public int ID { get; set; }
        public int SchnurauszeichnungId { get; set; }
        public int Anzahl { get; set; }
        public String Bezeichnung { get; set; }
    }
}
