using System;

namespace Logic.Messages.VereinsmeisterschaftMessages
{
    public class NeueVereinsmeisterschaftErstellenMessage
    {
        public Action<bool> Callback { get; private set; }

        public NeueVereinsmeisterschaftErstellenMessage(Action<bool> callback)
        {
            Callback = callback;
        }
    }
}
