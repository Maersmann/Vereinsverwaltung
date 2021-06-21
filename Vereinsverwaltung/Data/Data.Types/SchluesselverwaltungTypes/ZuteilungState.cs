using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Types.Converter;

namespace Data.Types.SchluesselverwaltungTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ZuteilungState
    {
        Zuteilung = 0,
        [Description("Rückgabe")]
        Rueckgabe = 1
    }
}
