using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.SchnurschiessenMessages
{
    public class OpenAuszeichnungGekauftEintragenMessage
    {
        public int SchnurauszeichnungId { get; set; }
        public string Bezeichnung { get; set; }
    }
}
