using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.AuswahlModels
{
    public class SchnurschiessenRangAuswahlModel
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public int Rang { get; set; }
        public int AuszeichnungID { get; set; }
        public string AuszeichnungBezeichnung { get; set; }
        public bool NeueStufe { get; set; }
        public bool DarfAuszeichnungBehalten { get; set; }
    }
}
