using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vereinsverwaltung.Data.Model.SchluesselEntitys
{
    [Table("Schluesselzuteilung")]
    public class Schluesselzuteilung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int SchluesselID { get; set; }
        public int SchluesselbesitzerID { get; set; }
        public int Anzahl { get; set; }
        public DateTime ErhaltenAm { get; set; }
        
        public virtual Schluessel Schluessel { get; set; }
        public virtual Schluesselbesitzer Schluesselbesitzer { get; set; }


    }
}
