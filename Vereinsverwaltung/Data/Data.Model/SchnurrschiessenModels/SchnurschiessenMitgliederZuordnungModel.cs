using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class SchnurschiessenMitgliederZuordnungModel
    {
        public string Name {  get; set; }
        public string Vorname {  get; set; }
        public string AktuellerRang { get; set; }
        public int MitgliedID {  get; set; }
        public int? LetzteTeilnahme {  get; set; }
    }
}
