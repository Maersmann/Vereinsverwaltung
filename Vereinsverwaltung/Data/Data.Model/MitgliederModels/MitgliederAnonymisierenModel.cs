using Data.Types.MitgliederTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.MitgliederModels
{
    public class MitgliederAnonymisierenModel
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public DateTime? Geburtstag { get; set; }
        public String Ort { get; set; }
        public String Straße { get; set; }
        public Geschlecht Geschlecht { get; set; }
        public DateTime? Austrittsdatum { get; set; }
        public MitgliedEhemaligStatus MitgliedEhemaligStatus { get; set; }
    }
}
