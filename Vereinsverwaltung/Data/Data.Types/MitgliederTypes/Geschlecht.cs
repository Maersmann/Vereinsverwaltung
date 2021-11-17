using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Types.MitgliederTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Geschlecht
    {
        [Description("Männlich")]
        maennlich = 0,
        [Description("Weiblich")]
        weiblich = 1,
        [Description("Divers")]
        divers = 2
    }
}
