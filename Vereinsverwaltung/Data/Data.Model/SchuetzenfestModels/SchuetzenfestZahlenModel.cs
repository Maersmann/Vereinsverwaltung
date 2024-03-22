using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.SchuetzenfestModels
{
    public class SchuetzenfestZahlenModel
    {
        public int ID { get; set; }
        public int Jahr { get; set; }
        public int? AnzahlUmzugSonntag { get; set; }
        public int? AnzahlUmzugMontagVormittag { get; set; }
        public int? AnzahlUmzugMontagNachmittag { get; set; }
        public int? BaendchenSamstagMitglieder { get; set; }
        public int? BaendchenSamstagGaeste { get; set; }
        public int? BaendchenMontagMitglieder { get; set; }
        public int? BaendchenMontagGaeste { get; set; }
    }
}
