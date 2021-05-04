using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.MitgliederModels
{
    public class MitgliedStammdatenModel
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Ort { get; set; }
        public string Straße { get; set; }
        public int? Mitgliedsnr { get; set; }
        public DateTime? Eintrittsdatum { get; set; }
        public DateTime? Geburtstag { get; set; }
    }
}
