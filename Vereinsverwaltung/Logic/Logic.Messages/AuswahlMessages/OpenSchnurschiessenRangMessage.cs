using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.AuswahlMessages
{
    public class OpenSchnurschiessenRangMessage
    {
        public Action<bool, int> Callback { get; private set; }

        public OpenSchnurschiessenRangMessage(Action<bool, int> callback)
        {
            Callback = callback;
        }
    }
}
