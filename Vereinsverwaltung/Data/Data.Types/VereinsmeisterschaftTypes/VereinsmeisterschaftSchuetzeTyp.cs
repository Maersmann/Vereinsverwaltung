using Data.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types.VereinsmeisterschaftTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum VereinsmeisterschaftSchuetzeTyp
    {
        [Description("Unbekannt")]
        unbekannt = 0,
        [Description("Männer 16-30")]
        maennlich16_30 = 1,
        [Description("Männer 31-50")]
        maennlich31_50 = 2,
        [Description("Männer 50-")]
        maennlich51 = 3,
        [Description("Frauen")]
        weiblich = 4,
        [Description("Sportschützen")]
        sportschuetzen = 5
    }
}
