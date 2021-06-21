using Data.Model.PinModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.AuswahlModels
{
    public class PinAusgabeAuswahlModel
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public PinModel Pin { get; set; }
    }
}
