using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class JugendkoenigschiessenUebersichtModel
    {
        public int Jahr { get; set; }
        public int AnzahlJugendkoenig { get; set; }
        public int AnzahlJugendkoenigin { get; set; }   
        public int RundeJugendkoenig { get; set; }
        public int RundeJugendkoenigin { get; set; }

        public bool AnmeldungMoeglich { get; set; }
        public bool JugenkoenigschiessenNichtBeendet { get; set; }
        public bool JugenkoeniginschiessenNichtBeendet { get; set; }
    }
}
