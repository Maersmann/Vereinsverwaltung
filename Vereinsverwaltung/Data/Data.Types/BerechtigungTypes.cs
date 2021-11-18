using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum BerechtigungTypes
    {
        Undefiniert = -1,
        [Description("Mitglieder Import")]
        MitgliederImport = 0,
        [Description("Schlüsselverwaltung")]
        Schluesselverwaltung = 1
    }
}
