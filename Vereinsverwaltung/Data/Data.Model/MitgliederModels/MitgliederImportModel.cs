using Data.Types.MitgliederTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.MitgliederModels
{
    public class MitgliederImportModel
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public DateTime? Eintrittsdatum { get; set; }
        public DateTime? Geburtstag { get; set; }
        public String Ort { get; set; }
        public String Straße { get; set; }
        public int Mitgliedsnr { get; set; }
        public MitgliedImportStateTypes State { get; set; }
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

        public Geschlecht Geschlecht { get; set; }

        public MitgliederImportModel()
        {
            Straße = "";
            Ort = "";
            Geschlecht = Geschlecht.maennlich;
            Vorname = "";
            Name = "";
        }
    }
}
