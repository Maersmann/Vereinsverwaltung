using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.AuswahlMessages
{
    public class OpenPinAusgabeAuswahlMessage
    {
        public Action<bool, int> Callback { get; private set; }

        public OpenPinAusgabeAuswahlMessage(Action<bool, int> callback)
        {
            Callback = callback;
        }
    }
}
