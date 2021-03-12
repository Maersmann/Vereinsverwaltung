using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vereinsverwaltung.Logic.Core.SchluesselCore.Models
{
    public class SchluesselRueckgabeStammdatenModel
    {
        public string Schluesselbezeichnung { get; set; }
        public string SchluesselbesitzerName { get; set; }
        public int SchluesselzuteilungID { get; set; }
        public DateTime RueckgabeAm { get; set; }
        public int Anzahl { get; set; }
    }
}
