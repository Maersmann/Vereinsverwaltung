using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class SchnurauszeichnungModel
    {
        public int ID { get; set; }
        public String Bezeichnung { get; set; }
        public int Rangfolge { get; set; }
        public int HauptteilID { get; set; }
        public int? ZusatzID { get; set; }
        public string HauptteilBezeichnung { get; set; }
        public string ZusatzBezeichnung { get; set; }
    }
}
