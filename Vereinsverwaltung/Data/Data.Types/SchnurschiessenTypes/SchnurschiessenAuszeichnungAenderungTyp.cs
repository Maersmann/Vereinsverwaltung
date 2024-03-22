using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types.SchnurschiessenTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]

    public enum SchnurschiessenAuszeichnungAenderungTyp
    {
        [Description("Aus Bestand erhalten")]

        AusBestand = 0,
        [Description("In Bestand zurück")]

        InBestand = 1,
        [Description("An Mitglied ausgegeben")]

        ausgabe = 2,
        [Description("Von Mitglied erhalten")]

        rueckgabeIntakt = 3
    }
}
