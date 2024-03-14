using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class AktivesSchnurschiessenVerwaltungAusgabeSchnurModel
    {
        public int SchnurschiessenBestandID { get; set; }
        public int SchnurschiessenAuszeichnungID { get; set; }
        public int Bestand { get; set; }
        public string Bezeichnung { get; set; }

        public AktivesSchnurschiessenVerwaltungAusgabeSchnurModel()
        {
            Bestand = 0;
        }
    }
}
