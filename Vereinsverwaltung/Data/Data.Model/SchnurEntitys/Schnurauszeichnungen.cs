using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vereinsverwaltung.Data.Model.SchnurEntitys
{
    [Table("Schnurauszeichnung")]
    public class Schnurauszeichnung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public String Bezeichnung { get; set; }
        public int Rangfolge { get; set; }
        public int HauptteilID { get; set; }
        public int? ZusatzID { get; set; }

        public virtual Schnur Hauptteil { get; set; }
        public virtual Schnur Zusatz { get; set; }
    }
}
