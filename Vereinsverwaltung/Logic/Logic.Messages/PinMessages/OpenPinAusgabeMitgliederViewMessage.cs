using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.PinMessages
{
    public class OpenPinAusgabeMitgliederViewMessage
    {
        public int ID { get; set; }
        public string FilterText { get; set; }
        public bool ZeigeNurOffene { get; set; }
    }
}
