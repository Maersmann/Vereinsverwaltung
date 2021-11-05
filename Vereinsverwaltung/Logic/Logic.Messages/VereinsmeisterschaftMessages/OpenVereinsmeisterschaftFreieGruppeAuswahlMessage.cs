using Data.Types.MitgliederTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Messages.VereinsmeisterschaftMessages
{
    public class OpenVereinsmeisterschaftFreieGruppeAuswahlMessage
    {
        public Action<bool, int> Callback { get; private set; }
        public int VereinsmeisterschaftID { get; set; }
        public Geschlecht? Geschlecht { get; set; }

        public OpenVereinsmeisterschaftFreieGruppeAuswahlMessage(Action<bool, int> callback, int vereinsmeisterschaftID, Geschlecht? geschlecht)
        {
            Callback = callback;
            VereinsmeisterschaftID = vereinsmeisterschaftID;
            Geschlecht = geschlecht;
        }
    }
}
