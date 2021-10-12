using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KkSchiessenModels
{
    public class KkSchiessenModel
    {
        public int ID { get; set; }
        public DateTime Datum { get; set; }
        public int Getraenke { get; set; }
        public int PackungenMunition { get; set; }
        public int KkSchiessGruppeID { get; set; }
        public virtual KkSchiessgruppeModel KkSchiessGruppe { get; set; }
    }
}
