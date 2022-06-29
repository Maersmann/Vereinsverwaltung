using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.KoenigschiessenMessages
{
    public class OpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage
    {
        public KoenigschiessenArt Art { get; set; }
        public int Jahr { get; set; }
        public int Runde { get; set; }
    }
}
