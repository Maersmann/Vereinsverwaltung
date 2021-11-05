using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.AuswahlMessages
{
    public class OpenSchiessgruppeAuswahlMessage
    {
        public Action<bool, int> Callback { get; private set; }

        public OpenSchiessgruppeAuswahlMessage(Action<bool, int> callback)
        {
            Callback = callback;
        }
    }
}
