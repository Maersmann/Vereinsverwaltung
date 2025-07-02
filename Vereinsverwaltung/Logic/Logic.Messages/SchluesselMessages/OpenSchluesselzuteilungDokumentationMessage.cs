using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Messages.SchluesselMessages
{
    public class OpenSchluesselzuteilungDokumentationMessage
    {
        public Action Command { get; set; }
        public int ID { get; set; }
        public bool DokumentationRueckgabeErstellt { get; set; }
        public bool DokumentationRueckgabeAbgeschlossen { get; set; }
        public bool DokumentationZuteilungErstellt { get; set; }
        public bool DokumentationZuteilungAbgeschlossen { get; set; }
    }
}
