using Data.Types.SchnurschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class SchnurModel
    {
        public int ID { get; set; }
        public String Bezeichnung { get; set; }
        public Schnurtypes Schnurtyp { get; set; }
        public Boolean Sichtbar { get; set; }
    }
}
