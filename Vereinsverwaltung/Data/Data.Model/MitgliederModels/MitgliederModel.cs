using Data.Types.MitgliederTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.MitgliederModels
{
    public class MitgliederModel
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

        public int? Alter
        {
            get
            {
                if (!Geburtstag.HasValue)
                    return null;
                else
                {
                    int years = DateTime.Now.Year - Geburtstag.Value.Year;
                    var birthday = Geburtstag.Value.AddYears(years);
                    if (DateTime.Now.CompareTo(birthday) < 0) { years--; }
                    return years;
                }
            }
        }

        public string Fullname => Vorname + " " + Name;
    }
}
