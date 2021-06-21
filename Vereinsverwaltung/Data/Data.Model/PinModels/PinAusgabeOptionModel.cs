using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.PinModels
{
    public class PinAusgabeOptionModel
    {
        public int ID { get; set; }
        public bool NurAktive { get; set; }
        public DateTime? Stichtag { get; set; }
    }
}
