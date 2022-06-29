using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels.DTOs
{
    public class KoenigschiessenRundeAbschliessenDTO
    {
        public int Jahr { get; set; }
        public int Runde { get; set; }
        public KoenigschiessenArt Art { get; set; }
    }
}
