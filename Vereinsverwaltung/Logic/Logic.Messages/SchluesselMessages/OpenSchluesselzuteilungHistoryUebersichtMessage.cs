using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Types.SchluesselverwaltungTypes;

namespace Logic.Messages.SchluesselMessages
{
    public class OpenSchluesselzuteilungHistoryUebersichtMessage
    {
        public int ID { get; set; }
        public SchluesselzuteilungTypes AuswahlTypes { get; set; }
    }
}
