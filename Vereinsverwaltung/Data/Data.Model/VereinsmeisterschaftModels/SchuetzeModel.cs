using Data.Types.MitgliederTypes;
using System;

namespace Data.Model.VereinsmeisterschaftModels
{
    public class SchuetzeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? Geburtstag { get; set; }
        public Geschlecht Geschlecht { get; set; }
        public bool Sportschuetze { get; set; }
        public int? MitgliedID { get; set; }
        public int? MitgliedsNr { get; set; }
    }
}
