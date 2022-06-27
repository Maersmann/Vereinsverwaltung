using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class KoenigschiessenUebersichtModel
    {
        public int Jahr { get; set; }
        public int AnzahlKoenig { get; set; }
        public int AnzahlVize { get; set; }
        public int AnzahlBesteSchuetzin { get; set; }
        public KoenigschiessenIntervall Intervall { get; set; }
        public int RundeKoenigschiessen { get; set; }
        public int RundeVizekoenigschiessen { get; set; }
        public int RundeBesteSchuetzin { get; set; }
        public bool AnmeldungMoeglich { get; set; }
        public bool KoenigschiessenNichtBeendet { get; set; }
        public bool VizekoenigschiessenNichtBeendet { get; set; }
        public bool BesteSchuetzinNichtBeendet { get; set; }
    }
}
