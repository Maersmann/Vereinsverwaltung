using Data.Types.VereinsmeisterschaftTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.VereinsmeisterschaftModels
{
    public class VereinsmeisterschaftGruppeErgebnisModel
    {
        public int ID { get; set; }
        public double Ergebnis { get; set; }
        public int? Platzierung { get; set; }
        public int VereinsmeisterschaftID { get; set; }
        public int SchiessgruppeID { get; set; }
        public VereinsmeisterschaftGruppeTyp VereinsmeisterschaftGruppeTyp { get; set; }
        public virtual VereinsmeisterschaftModel Vereinsmeisterschaft { get; set; }
        public virtual SchiessgruppeModel Schiessgruppe { get; set; }
        //public IList<VereinsmeisterschaftschuetzeErgebnisModel> VereinsmeisterschaftschuetzeErgebnisse { get; set; }
    }
}
