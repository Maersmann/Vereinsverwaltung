using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.AuswertungModels.SchuetzenfestAuswertungModels
{
    public class SchuetzenfestZahlenAuswertungUmzugModel
    {
        public int Jahr { get; set; }
        public int AnzahlSonntag { get; set; }
        public int AnzahlMontagvormittag { get; set; }
        public int AnzahlMontagnachmittag { get; set; }
    }
}
