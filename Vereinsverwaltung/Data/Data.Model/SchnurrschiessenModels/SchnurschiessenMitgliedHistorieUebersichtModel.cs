using Data.Types.SchnurschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class SchnurschiessenMitgliedHistorieUebersichtModel
    {
        public int Id { get; set; }
        public int Jahr { get; set; }
        public bool Erhalten { get; set; }
        public bool Rueckgabe { get; set; }
        public string Rang { get; set; }
        public DateTime? ErhaltenAm { get; set; }
        public DateTime? RueckgabeAm { get; set; }
        public SchnurrauszeichnungRueckgabeTyp RueckgabeTyp { get; set; }
        public bool CanZurueck { get; set; }
        public bool CanAusgeben { get; set; }
        public int MitgliedID { get; set; }
        public int SchnurschiessenrangID { get; set; }

    }
}
