using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Logic.Types.Converter;

namespace Data.Types.SchnurschiessenTypes
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Schnurtypes
    {
        [Description("Schnur")]
        schnur = 0,
        [Description("Eichel")]
        eichel = 1,
        [Description("Plakette")]
        plakette = 2
    }
}
