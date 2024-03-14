using Data.Types.SchnurschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class AktivesSchnurschiessenTeilnahmeEintragenRueckgabeModel
    {
        public string Bezeichnung { get; set; }
        public int ID {  get; set; }
        public SchnurrauszeichnungRueckgabeTyp RueckgabeTyp { get; set; }

        public bool CanRueckgabeTypAnpassen => !RueckgabeTyp.Equals(SchnurrauszeichnungRueckgabeTyp.nichtAusgegeben);

    }
}
