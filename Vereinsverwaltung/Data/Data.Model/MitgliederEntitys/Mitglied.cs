using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Data.Types.MitgliederTypes;

namespace Vereinsverwaltung.Data.Entitys.MitgliederEntitys
{
    [Table("Mitglied")]
    public class Mitglied
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Vorname { get; set; }
        public DateTime? Eintrittsdatum { get; set; }
        public DateTime? Geburtstag { get; set; }
        public String Ort { get; set; }

        [Column("Strasse")]
        public String Straße { get; set; }
        public int? Mitgliedsnr { get; set; }

        [EnumDataType(typeof(MitgliedStatus))]
        public MitgliedStatus MitgliedStatus { get; set; }
        [EnumDataType(typeof(Geschlecht))]
        public Geschlecht Geschlecht { get; set; }
        public DateTime? AusgetretenAm { get; set; }

        public virtual Schluesselbesitzer Schluesselbesitzer { get; set; }


        [NotMapped]
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

        [NotMapped]
        public string Fullname => Vorname + " " + Name;

        public Mitglied()
        {
            Straße = "";
            Ort = "";
            Geschlecht = Geschlecht.maennlich;
            Vorname = "";
            Name = "";
        }
    }
}
