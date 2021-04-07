using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vereinsverwaltung.Data.Model.ImportEntitys
{
    [Table("MitgliedImportHistory")]
    public class MitgliedImportHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime EingelesenAm { get; set; }
        public int GesamtEingelesen { get; set; }
        public int AnzahlNeu { get; set; }
        public int AnzahlEhemalig { get; set; }
        public int AnzahlKeineAenderung { get; set; }
        public int AnzahlAenderung { get; set; }
    }
}
