using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types.SchnurschiessenTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum SchnurschiessenImportStateType
    {
        [Description("OK")]
        ok = 1,
        [Description("Mitglieds-Nr fehlt")]
        mitgliedNrFehlt = 2,
        [Description("Mitglieds-Nr nicht vorhanden")]
        mitgliedNrNichtVorhanden = 3,
        [Description("Mitglieds-Name passt nicht")]
        mitgliedNamePasstNicht = 4,
        [Description("Rang nicht vorhanden")]
        rangNichtVorhanden = 5,
        [Description("Mitglied schon importiert")]
        mitgliedSchonImportiert = 6,

    }
}
