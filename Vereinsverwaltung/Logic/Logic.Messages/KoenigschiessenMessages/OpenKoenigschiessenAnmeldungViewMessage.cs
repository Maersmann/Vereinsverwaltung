using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.KoenigschiessenMessages
{
    public class OpenKoenigschiessenAnmeldungViewMessage
    {
        public int Jahr { get; set; }
        public KoenigschiessenVarianten Variante { get; set; }
    }
}
