using Data.Types.AllgemeintTypes;
using Data.Types.SchnurschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class AktiveSchnurschiessenBestandHistorieModel
    {
        public int ID { get; set; }
        public int SchnurschiessenBestandID { get; set; }
        public int Anzahl { get; set; }
        public DateTime Datum { get; set; }
        public SchnurschiessenAuszeichnungAenderungTyp SchnurschiessenAuszeichnungAenderung { get; set; }
        public AenderungTypes Aenderung { get; set; }
    }
}
