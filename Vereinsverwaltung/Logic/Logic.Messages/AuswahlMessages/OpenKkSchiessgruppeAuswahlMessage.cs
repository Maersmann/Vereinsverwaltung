using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.AuswahlMessages
{
    public class OpenKkSchiessgruppeAuswahlMessage
    {
        public Action<bool, int> Callback { get; private set; }

        public OpenKkSchiessgruppeAuswahlMessage(Action<bool, int> callback)
        {
            Callback = callback;
        }
    }
}
