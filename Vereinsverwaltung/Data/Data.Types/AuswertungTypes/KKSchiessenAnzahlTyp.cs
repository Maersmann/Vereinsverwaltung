using Data.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types.AuswertungTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum KKSchiessenAnzahlTyp
    {
        [Description("Veranstaltung")]
        veranstaltung,
        [Description("Getränke")]
        getraenke,
        [Description("Munition")]
        munition
    }
}
