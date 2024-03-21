using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.AuswertungModels.SchuetzenfestAuswertungModels
{
    public class SchuetzenfestzahlenAuswertungBaendchenModel
    {
        public int Jahr { get; set; }
        public int BaendchenSamstagMitglieder { get; set; }
        public int BaendchenSamstagGaeste { get; set; }
        public int BaendchenMontagMitglieder { get; set; }
        public int BaendchenMontagGaeste { get; set; }
    }
}
