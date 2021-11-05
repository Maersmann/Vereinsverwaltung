using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.VereinsmeisterschaftMessages
{
    public class NeuerSchuetzeErstellenMessage
    {
        public Action<bool> Callback { get; private set; }
        public int VereinsmeisterschaftID { get; set; }

        public NeuerSchuetzeErstellenMessage(Action<bool> callback, int vereinsmeisterschaftID)
        {
            Callback = callback;
            VereinsmeisterschaftID = vereinsmeisterschaftID;
        }
    }
}
