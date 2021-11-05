using Data.Types.VereinsmeisterschaftTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.VereinsmeisterschaftMessages
{
    public class LoadVereinsmeisterschaftAktivErgebnisseVonGruppentypMessage
    {
        public VereinsmeisterschaftGruppeTyp GruppeTyp { get; set; }
        public int VereinsmeisterschaftID { get; set; }
    }
}
