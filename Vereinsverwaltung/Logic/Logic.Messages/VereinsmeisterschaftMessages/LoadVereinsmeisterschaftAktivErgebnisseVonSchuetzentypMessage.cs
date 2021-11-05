using Data.Types.VereinsmeisterschaftTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.VereinsmeisterschaftMessages
{
    public class LoadVereinsmeisterschaftAktivErgebnisseVonSchuetzentypMessage
    {
        public VereinsmeisterschaftSchuetzeTyp SchuetzeTyp { get; set; }
        public int VereinsmeisterschaftID { get; set; }
    }
}
