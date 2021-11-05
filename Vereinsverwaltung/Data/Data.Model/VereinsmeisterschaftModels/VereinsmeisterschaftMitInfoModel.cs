using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.VereinsmeisterschaftModels
{
    public class VereinsmeisterschaftMitInfoModel
    {
        public int ID { get; set; }
        public int Jahr { get; set; }
        public int AnzahlSchuetzen { get; set; }
        public int AnzahlGruppen { get; set; }
        public int AnzahlGruppenMaenner { get; set; }
        public int AnzahlGruppenFrauen { get; set; }
        public int AnzahlFrauen { get; set; }
        public int AnzahlMaenner16_30 { get; set; }
        public int AnzahlMaenner31_50 { get; set; }
        public int AnzahlMaenner51 { get; set; }
        public int AnzahlSportschuetzen { get; set; }

        public VereinsmeisterschaftMitInfoModel()
        {
            Jahr = DateTime.Now.Year;
            AnzahlSportschuetzen = 0;
            AnzahlSchuetzen = 0;
            AnzahlMaenner51 = 0;
            AnzahlMaenner31_50 = 0;
            AnzahlMaenner16_30 = 0;
            AnzahlGruppenMaenner = 0;
            AnzahlFrauen = 0;
            AnzahlGruppenFrauen = 0;
            AnzahlGruppen = 0;
            
        }
    }
}
