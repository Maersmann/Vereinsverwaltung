using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class KoenigschiessenErstellenModel
    {
        public DateTime? Stichtag { get; set; }
        public KoenigschiessenIntervall Intervall { get; set; }
    }
}
