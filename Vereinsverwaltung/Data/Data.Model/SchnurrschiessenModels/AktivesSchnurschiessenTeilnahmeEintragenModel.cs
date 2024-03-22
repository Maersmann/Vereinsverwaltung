using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class AktivesSchnurschiessenTeilnahmeEintragenModel
    {
        public int MitgliedID { get; set; }
        public string Name { get; set; }
        public string AlterRang {  get; set; }
        public string NeuerRang {  get; set; }  
        public string ZuerhalteneAuszeichung {  get; set; }
        public int NeuerRangID {  get; set; }
        public bool AuszeichnungAusgegeben {  get; set; }
        public bool DarfNaechstenAusgeben { get; set; }

        public ObservableCollection<AktivesSchnurschiessenTeilnahmeEintragenRueckgabeModel> Auszeichnungen { get; set; }
    }
}
