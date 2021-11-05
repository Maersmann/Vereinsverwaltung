using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.VereinsmeisterschaftModels
{
    public class VereinsmeisterschaftErgebnisEintragenModel
    {
        public string Name { get; set; }
        public int SchuetzenergebnisID { get; set; }
        public double Ergebnis { get; set; }

        public VereinsmeisterschaftErgebnisEintragenModel()
        {
            Ergebnis = 0;
        }
    }
}
