using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.KoenigschiessenMessages
{
    public class OpenKoenigschiessenRundeTeilnehmerUebersichtViewMessage
    {
        public KoenigschiessenArt Art { get; set; }
        public int Jahr { get; set; }
        public KoenigschiessenVarianten Variante { get; set; }
        public int Runde { get; set; }
    }
}
