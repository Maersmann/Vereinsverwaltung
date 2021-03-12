using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vereinsverwaltung.Logic.Core.SchluesselCore.Models
{
    public class SchluesselzuteilungSchluesselModel
    {
        public int Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public int AnzahlFrei { get; set; }
        public int AnzahlGesamt { get; set; }
        public int Ausgegeben { get; set; }
        public int SchluesselID { get; set; }
    }
}
