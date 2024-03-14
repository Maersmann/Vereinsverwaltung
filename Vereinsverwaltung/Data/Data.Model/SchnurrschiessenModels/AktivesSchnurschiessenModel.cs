using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class AktivesSchnurschiessenModel
    {
        public int ID { get; set; }
        public DateTime Erstelldatum { get; set; }
        public DateTime? AbgeschlossenAm { get; set; }
        public int Jahr { get; set; }
        public bool Aktiv { get; set; }
    }
}
