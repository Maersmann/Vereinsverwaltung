using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Messages.AuswahlMessages
{
    public class OpenMitgliedAuswahlMessage
    {
        public Action<bool, int> Callback { get; private set; }

        public OpenMitgliedAuswahlMessage( Action<bool,int> callback)
        {
            Callback = callback;
        }
    }
}
