using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vereinsverwaltung.Data.Model.SchluesselEntitys
{
    [Table("Schluessel")]
    public class Schluessel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public String Bezeichnung { get; set; }
        public String Beschreibung { get; set; }
        public int Nummer { get; set; }
        public int Bestand { get; set; }
        public int Ausgegeben { get; set; }
        public List<Schluesselzuteilung> Verteilung { get; set; }

        [NotMapped]
        public int FreieAnzahl { get { return (Bestand - Ausgegeben); } }
        [NotMapped]
        public bool CanRueckgabe { get { return Ausgegeben > 0; } }
    }
}
