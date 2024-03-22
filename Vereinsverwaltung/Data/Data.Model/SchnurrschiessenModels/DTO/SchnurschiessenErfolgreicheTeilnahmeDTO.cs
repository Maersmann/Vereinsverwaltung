using Data.Types.SchnurschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels.DTO
{
    public class SchnurschiessenErfolgreicheTeilnahmeDTO
    {
        public int MitgliedID { get; set; }
        public int NeuerRangID { get; set; }
        public bool AuszeichnungAusgegeben { get; set; }
        public IList<SchnurauszeichnungRueckgabeDTO> SchnurauszeichnungRueckgaben {  get; set; }

        public SchnurschiessenErfolgreicheTeilnahmeDTO()
        {
            SchnurauszeichnungRueckgaben = new List<SchnurauszeichnungRueckgabeDTO>();
        }
    }

    public class SchnurauszeichnungRueckgabeDTO
    {
        public int ID { get; set; }
        public SchnurrauszeichnungRueckgabeTyp RueckgabeTyp { get; set; }
    }
}
