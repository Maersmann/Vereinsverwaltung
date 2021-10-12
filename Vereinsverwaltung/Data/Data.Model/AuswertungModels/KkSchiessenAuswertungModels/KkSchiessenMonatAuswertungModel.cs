using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.AuswertungModels.KkSchiessenAuswertungModels
{
    public class KkSchiessenMonatAuswertungModel
    {
        public DateTime Datum { get; set; }
        public int Anzahl { get; set; }
        public int Getraenke { get; set; }
        public int Munition { get; set; }
    }
}
