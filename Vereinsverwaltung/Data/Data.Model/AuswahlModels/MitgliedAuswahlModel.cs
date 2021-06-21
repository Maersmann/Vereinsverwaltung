using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.AuswahlModels
{
    public class MitgliedAuswahlModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public DateTime? Eintrittsdatum { get; set; }
        public int? Mitgliedsnr { get; set; }
        public string Fullname => Vorname + " " + Name;
    }
}
