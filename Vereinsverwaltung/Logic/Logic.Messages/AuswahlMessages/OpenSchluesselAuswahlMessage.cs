using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vereinsverwaltung.Logic.Messages.AuswahlMessages
{
    public class OpenSchluesselAuswahlMessage
    {
        public Action<bool, int> Callback { get; private set; }

        public OpenSchluesselAuswahlMessage(Action<bool, int> callback)
        {
            Callback = callback;
        }
    }
}
