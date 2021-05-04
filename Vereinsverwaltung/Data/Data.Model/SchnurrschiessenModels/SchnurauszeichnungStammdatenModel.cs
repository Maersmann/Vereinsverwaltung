using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class SchnurauszeichnungStammdatenModel
    {
        public object HauptteilID { get; set; }
        public object ZusatzID { get; set; }
        public string Bezeichnung { get; set; }
        public int Rangfolge { get; set; }
    }
}
