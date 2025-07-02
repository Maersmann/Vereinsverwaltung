using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Messages.SchluesselMessages
{
    public class OpenSchluesselKennungEintragenMessage
    {
        public Action Command { get; set; }

        public int ID { get; set; }
    }
}
