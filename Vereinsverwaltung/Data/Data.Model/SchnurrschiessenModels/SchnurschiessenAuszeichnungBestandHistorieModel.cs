using Data.Types.AllgemeintTypes;
using Data.Types.SchnurschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class SchnurschiessenAuszeichnungBestandHistorieModel
    {
        public int Id { get; set; }
        public int Anzahl { get; set; }
        public DateTime Datum { get; set; }
        public BestandAenderungTyp BestandAenderung { get; set; }
        public AenderungTypes Aenderung { get; set; }
    }
}
