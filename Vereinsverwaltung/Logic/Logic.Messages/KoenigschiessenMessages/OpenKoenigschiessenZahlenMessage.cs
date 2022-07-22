using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.KoenigschiessenMessages
{
    public class OpenKoenigschiessenZahlenMessage
    {
        public KoenigschiessenVarianten Variante { get; set; }
        public int Jahr { get; set; }
    }
}
