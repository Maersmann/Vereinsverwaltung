using Data.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum PageSizeTypes
    {
        [Description("5")]
        size5 = 5,
        [Description("25")]
        size25 = 25,
        [Description("50")]
        size50 = 50,
        [Description("100")]
        size100 = 100
    }
}
