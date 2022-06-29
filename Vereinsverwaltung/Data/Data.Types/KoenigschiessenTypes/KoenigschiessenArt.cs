using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types.KoenigschiessenTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum KoenigschiessenArt
    {
        [Description("Jugendkönig")]
        jugendkoenig = 0,
        [Description("Jugendkönigin")]
        jugendkoenigin = 1,
        [Description("Beste Schützin")]
        besteSchuetzin = 2,
        [Description("Vize-König")]
        vizekoenig = 3,
        [Description("König")]
        koenig = 4,
        [Description("Ohne Wertung")]
        ohneWertung = 5
    }
}
