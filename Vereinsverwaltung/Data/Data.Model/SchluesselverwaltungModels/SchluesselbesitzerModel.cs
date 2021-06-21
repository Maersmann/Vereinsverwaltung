using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchluesselverwaltungModels
{
    public class SchluesselbesitzerModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? MitgliedsNr { get; set; }
        public int? MitgliedID { get; set; }
        public bool CanRueckgabe { get; set; }
    }
}
