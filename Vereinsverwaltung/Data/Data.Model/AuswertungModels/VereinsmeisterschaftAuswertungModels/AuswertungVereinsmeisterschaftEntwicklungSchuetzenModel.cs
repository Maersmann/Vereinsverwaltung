using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.AuswertungModels.VereinsmeisterschaftAuswertungModels
{
    public class AuswertungVereinsmeisterschaftEntwicklungSchuetzenModel
    {
        public int Jahr { get; set; }
        public int AnzahlFrauen { get; set; }
        public int AnzahlMaenner16_30 { get; set; }
        public int AnzahlMaenner31_50 { get; set; }
        public int AnzahlMaenner51 { get; set; }
        public int AnzahlSportschuetzen { get; set; }
    }
}
