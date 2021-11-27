using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types.VereinsmeisterschaftTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum VereinsmeisterschaftGruppeTyp
    {
        [Description("Unbekannt")]
        unbekannt = 0,
        [Description("Männer")]
        maennlich = 1,
        [Description("Frauen")]
        weiblich = 2
    }
}
