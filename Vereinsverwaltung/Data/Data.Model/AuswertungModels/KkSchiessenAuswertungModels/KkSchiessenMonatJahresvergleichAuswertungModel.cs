using Data.Types.AuswertungTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.AuswertungModels.KkSchiessenAuswertungModels
{
    public class KkSchiessenMonatJahresvergleichAuswertungModel
    {
        public KKSchiessenAnzahlTyp Typ { get; set; }
        public int Jahr { get; set; }
        public IList<KkSchiessenMonatJahresvergleichAuswertungMonatsWertModel> Monatswerte { get; set; }

        public KkSchiessenMonatJahresvergleichAuswertungModel()
        {
            Monatswerte = new List<KkSchiessenMonatJahresvergleichAuswertungMonatsWertModel>();
        }
    }

    public class KkSchiessenMonatJahresvergleichAuswertungMonatsWertModel
    {
        public int Monat { get; set; }
        public int Anzahl { get; set; }
    }
}
