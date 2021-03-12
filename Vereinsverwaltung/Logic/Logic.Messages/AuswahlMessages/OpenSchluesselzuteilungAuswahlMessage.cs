using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Types.SchluesselverwaltungTypes;

namespace Vereinsverwaltung.Logic.Messages.AuswahlMessages
{
    public class OpenSchluesselzuteilungAuswahlMessage
    {
        public int ID { get; set; }
        public SchluesselzuteilungTypes AuswahlTypes { get; set; }
        public Action<bool, int> Callback { get; private set; }

        public OpenSchluesselzuteilungAuswahlMessage(Action<bool, int> callback, int id, SchluesselzuteilungTypes auswahlTypes)
        {
            Callback = callback;
            ID = id;
            AuswahlTypes = auswahlTypes;
        }
    }
}
