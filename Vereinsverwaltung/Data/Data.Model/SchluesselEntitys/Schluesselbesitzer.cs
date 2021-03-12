using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Entitys.MitgliederEntitys;

namespace Vereinsverwaltung.Data.Model.SchluesselEntitys
{
    [Table("Schluesselbesitzer")]
    public class Schluesselbesitzer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? MitgliedID { get; set; }
        public string Name { get; set; }

        public virtual Mitglied Mitglied { get; set; }
        public List<Schluesselzuteilung> Verteilung { get; set; }
        [NotMapped]
        public bool CanRueckgabe { get { return Verteilung.Count > 0; } }

    }
}
