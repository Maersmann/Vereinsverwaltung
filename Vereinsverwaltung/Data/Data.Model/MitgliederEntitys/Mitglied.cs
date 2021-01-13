using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vereinsverwaltung.Data.Model.MitgliederEntitys
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


        [NotMapped]
        public int? Alter
        {
            get
            {
                if (!Eintrittsdatum.HasValue)
                    return null;
                else
                {
                    int years = DateTime.Now.Year - Eintrittsdatum.Value.Year;
                    var birthday = Eintrittsdatum.Value.AddYears(years);
                    if (DateTime.Now.CompareTo(birthday) < 0) { years--; }
                    return years;
                }
            }
        }

    }
}
