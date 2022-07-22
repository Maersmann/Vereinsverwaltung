using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class KoenigschiessenErgebnisseVonMitgliedModel
    {
        public int Jahr { get; set; }
        public string Runde { get; set; }
        public KoenigschiessenArt Art {get; set; }
    }
}
