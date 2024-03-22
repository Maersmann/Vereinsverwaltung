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

    public enum MitgliedEhemaligStatus
    {
        [Description("OK")]
        ok = 1,
        [Description("Schlüssel vorhanden")]
        schluesselVorhanden = 2,
    }
}
