using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Types.SchnurschiessenTypes;

namespace Vereinsverwaltung.Data.Model.SchnurEntitys
{
    [Table("Schnur")]
    public class Schnur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public String Bezeichnung { get; set; }
        public Schnurtypes Schnurtyp { get; set; }
    }
}
