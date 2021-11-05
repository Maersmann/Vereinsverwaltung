using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.VereinsmeisterschaftModels
{
    public class VereinsmeisterschaftGruppeVerfügbarkeitModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int FreiePlaetze { get; set; }
        public int Vergeben { get; set; }
        public int Gesamt { get; set; }
    }
}
