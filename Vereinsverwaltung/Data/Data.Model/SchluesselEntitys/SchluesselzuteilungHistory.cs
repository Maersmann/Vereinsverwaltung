
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Types.SchluesselverwaltungTypes;

namespace Vereinsverwaltung.Data.Model.SchluesselEntitys
{
    [Table("SchluesselzuteilungHistory")]
    public class SchluesselzuteilungHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int SchluesselID { get; set; }
        public int SchluesselbesitzerID { get; set; }
        [ForeignKey("asd")]
        public int? SchluesselzuteilungID { get; set; }

        public DateTime Datum { get; set; }
        public int Anzahl { get; set; }
        public ZuteilungState State { get; set; }

        public virtual Schluessel Schluessel { get; set; }
        public virtual Schluesselbesitzer Schluesselbesitzer { get; set; }
        public virtual Schluesselzuteilung Schluesselzuteilung { get; set; }
    }
}
