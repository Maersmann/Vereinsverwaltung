using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vereinsverwaltung.Logic.Core.SchluesselCore.Models
{
    public class SchluesselzuteilungBesitzerModel
    {
        public string Name { get; set; }
        public int Anzahl { get; set; }
        public int SchluesselbesitzerID { get; set; }
    }
}
