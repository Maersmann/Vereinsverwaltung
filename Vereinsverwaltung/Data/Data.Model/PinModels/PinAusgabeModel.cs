using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.PinModels
{
    public class PinAusgabeModel
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public string Erstelldatum { get; set; }
        public bool Abgeschlossen { get; set; }
        public int PinID { get; set; }
        public int OptionID { get; set; }
        public virtual PinModel Pin { get; set; }
        public virtual PinAusgabeOptionModel Option { get; set; }
    }
}
