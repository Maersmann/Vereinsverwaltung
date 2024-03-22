using Data.Types.SchnurschiessenTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{

    public class AktivesSchnurschiessenSchnurBestandModel
    {
        public int SchnurschiessenBestandID { get; set; }
        public int SchnurschiessenAuszeichnungID { get; set; }
        public int Bestand { get; set; }
        public string Bezeichnung { get; set; }
        public int AnzahlAusgegeben { get; set; }
        public int AnzahlZurueck { get; set; }
        public int AnzahlBeschaedigt { get; set; }
        public int AnzahlVerloren { get; set; }
    }
}
