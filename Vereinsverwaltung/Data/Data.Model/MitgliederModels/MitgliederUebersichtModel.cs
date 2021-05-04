using Data.Types.MitgliederTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.MitgliederModels
{
    public class MitgliederUebersichtModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public DateTime? Eintrittsdatum { get; set; }
        public DateTime? Geburtstag { get; set; }
        public String Ort { get; set; }
        public String Straße { get; set; }
        public int? Mitgliedsnr { get; set; }
        public MitgliedStatus MitgliedStatus { get; set; }
    }
}
