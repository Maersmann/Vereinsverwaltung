using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchluesselverwaltungModels
{
    public class SchluesselverteilungSchluesselUebersichtModel
    {
        public int Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public int AnzahlFrei { get; set; }
        public int AnzahlGesamt { get; set; }
        public int Ausgegeben { get; set; }
        public int SchluesselID { get; set; }
        public bool CanZuteilen => AnzahlFrei > 0;
    }
}
