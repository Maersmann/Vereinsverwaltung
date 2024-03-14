using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class SchnurschiessenMitgliederUebersichtModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rang { get; set; }
        public int? LetztesJahr { get; set; }
        public string NaechsteRang { get; set; }
    }
}
