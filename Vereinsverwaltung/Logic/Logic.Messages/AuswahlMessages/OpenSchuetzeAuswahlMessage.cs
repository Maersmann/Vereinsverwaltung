using Data.Types.AuswahlTypes;
using Data.Types.MitgliederTypes;
using System;

namespace Logic.Messages.AuswahlMessages
{
    public class OpenSchuetzeAuswahlMessage
    {
        public int? VereinsmeisterschaftID { get; set; }
        public Geschlecht? Geschlecht { get; set; }
        public AuswahlVereinsmeisterschaftSchuetzeTypes AuswahlTyp { get; set; }
        public Action<bool, int> Callback { get; private set; }

        public OpenSchuetzeAuswahlMessage(Action<bool, int> callback, AuswahlVereinsmeisterschaftSchuetzeTypes auswahltyp)
        {
            Callback = callback;
            AuswahlTyp = auswahltyp;
        }
    }
}
