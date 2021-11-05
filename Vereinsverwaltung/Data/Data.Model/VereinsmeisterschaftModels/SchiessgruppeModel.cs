using Data.Types.MitgliederTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.VereinsmeisterschaftModels
{
    public class SchiessgruppeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Geschlecht Geschlecht { get; set; }
    }
}
