using Base.Logic.Types.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Types.SchnurschiessenTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]

    public enum SchnurrauszeichnungRueckgabeTyp
    {
        [Description("Rückgabe offen")]
        RueckgabeOffen = 0,
        [Description("Rückgabe erfolgt")]
        RueckgabeErfolgt = 1,
        [Description("Beschädigt")]
        beschaedigt = 2,
        [Description("Verloren")]
        verloren = 3,
        [Description("Nicht ausgegeben")]
        nichtAusgegeben = 4
    }
}
