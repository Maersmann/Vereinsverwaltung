using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Types.Converter;

namespace Vereinsverwaltung.Data.Types.MitgliederTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum MitgliedStatus
    {
        Aktiv = 0,
        Ehemalig = 1,
        [Description("Nicht Aktiv")]
        NichtAktiv = 2
    }
}
