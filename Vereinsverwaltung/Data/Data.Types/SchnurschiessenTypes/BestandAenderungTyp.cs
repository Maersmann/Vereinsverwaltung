using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types.SchnurschiessenTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum BestandAenderungTyp
    {
        [Description("Initial")]
        initial = 0,
        [Description("Einkauf")]
        einkauf = 1,
        [Description("Ausgabe - Schnurschiessen")]
        ausgabeSchnurschiessen = 2,
        [Description("Rückgabe - Schnurschiessen")]
        rueckgabeSchnurschiessen = 3,
        [Description("Ausgabe - Mitglied")]
        ausgabeMitglied = 4,
        [Description("Rückgabe - Mitglied")]
        rueckgabeMitglied = 5
    }
}
