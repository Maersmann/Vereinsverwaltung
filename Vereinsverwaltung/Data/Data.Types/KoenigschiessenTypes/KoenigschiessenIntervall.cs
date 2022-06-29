using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types.KoenigschiessenTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum KoenigschiessenIntervall
    {
        [Description("Jung")]
        jung = 0,
        [Description("Alt")]
        alt = 1
    }
}
