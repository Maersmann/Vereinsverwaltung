using Data.Types.AuswahlTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.AuswahlMessages
{
    public class OpenVereinsmeisterschaftAuswahlMessage
    {
        public AuswahlVereinsmeisterschaftTypes AuswahlTyp { get; set; }
        public Action<bool, int, int> Callback { get; private set; }

        public OpenVereinsmeisterschaftAuswahlMessage(Action<bool, int, int> callback)
        {
            Callback = callback;
        }
    }
}
