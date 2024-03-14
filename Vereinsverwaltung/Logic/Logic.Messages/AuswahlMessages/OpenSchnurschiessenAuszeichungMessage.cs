using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.AuswahlMessages
{
    public class OpenSchnurschiessenAuszeichungMessage
    {
        public Action<bool, int> Callback { get; private set; }

        public OpenSchnurschiessenAuszeichungMessage(Action<bool, int> callback)
        {
            Callback = callback;
        }
    }
}
