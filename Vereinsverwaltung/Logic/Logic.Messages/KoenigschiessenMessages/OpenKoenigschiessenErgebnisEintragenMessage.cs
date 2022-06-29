using Data.Model.KoenigschiessenModels;
using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.KoenigschiessenMessages
{
    public class OpenKoenigschiessenErgebnisEintragenMessage
    {
        public Action Command { get; set; }
        public KoenigschiessenRundeTeilnehmerUebersichtModel KoenigschiessenRundeTeilnehmer { get; set; }
        public KoenigschiessenVarianten variante { get; set; }
    }
}
