using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Logic.Types.Converter;

namespace Data.Types.MitgliederTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum MitgliedImportStateTypes
    {
        [Description("Neu")]
        New = 0,
        [Description("Änderung")]
        Update = 1,
        [Description("Keine Änderung")]
        NoUpdate = 2,
        [Description("Ehemalig")]
        Former = 3
    }
}
