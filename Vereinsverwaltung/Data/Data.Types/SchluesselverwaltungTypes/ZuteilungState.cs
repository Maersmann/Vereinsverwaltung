using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Types.Converter;

namespace Vereinsverwaltung.Data.Types.SchluesselverwaltungTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ZuteilungState
    {
        Zuteilung = 0,
        [Description("Rückgabe")]
        Rueckgabe = 1
    }
}
