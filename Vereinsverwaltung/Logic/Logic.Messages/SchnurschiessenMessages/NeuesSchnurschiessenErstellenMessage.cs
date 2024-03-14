using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.SchnurschiessenMessages
{
    
    public class NeuesSchnurschiessenErstellenMessage
    {
        public Action<bool> Callback { get; private set; }
        public NeuesSchnurschiessenErstellenMessage(Action<bool> callback)
        {
            Callback = callback;
        }
    }
}
