using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types.AllgemeintTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum AenderungTypes
    {
        [Description("Minderung")]
        minderung = 0,
        [Description("Erhöhung")]
        erhoehung = 1,
    }
}
