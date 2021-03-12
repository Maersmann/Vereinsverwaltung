using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Types;

namespace Vereinsverwaltung.Logic.Messages.BaseMessages
{
    public class BaseStammdatenMessage
    {
        public StammdatenTypes Stammdaten { get; set; }
        public State State { get; set; }
        public int? ID { get; set; }
    }
}
