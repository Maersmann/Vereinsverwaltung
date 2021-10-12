using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.Validierungen.Base
{
    public class BaseValidierung
    {
        public bool ValidateBetrag(Double? betrag, out ICollection<string> validatonErrors)
        {
            validatonErrors = new List<String>();

            if (!betrag.HasValue)
                validatonErrors.Add("Kein Betrag hinterlegt sein");

            if ((betrag == 0))
                validatonErrors.Add("Der Betrag darf nicht 0 sein");

            return validatonErrors.Count == 0;
        }

        public bool ValidateDatum(DateTime? date, out ICollection<string> validationErrors)
        {
            validationErrors = new List<String>();

            if (date.GetValueOrDefault(DateTime.MinValue).Equals(DateTime.MinValue))
                validationErrors.Add("Kein Datum hinterlegt");

            return validationErrors.Count == 0;
        }

        public bool ValidateString(String str , string fieldname, out ICollection<string> validationErrors)
        {
            validationErrors = new List<String>();

            if (fieldname.Length == 0)
                fieldname = "Text";

            if (str.Length == 0)
                validationErrors.Add("Kein "+ fieldname + " hinterlegt");

            return validationErrors.Count == 0;
        }

        public bool ValidateAnzahl(int? anzahl, out ICollection<string> validationErrors)
        {
            validationErrors = new List<String>();

            if (!anzahl.HasValue)
                validationErrors.Add("Keine Anzahl hinterlegt");

            if (anzahl == 0)
                validationErrors.Add("Die Anzahl darf nicht 0 sein");

            if (anzahl < 0)
                validationErrors.Add("Die Anzahl zu niedrig");

            return validationErrors.Count == 0;
        }

        public bool ValidateInteger(int? itg, out ICollection<string> validationErrors)
        {
            validationErrors = new List<String>();

            if (!itg.HasValue)
                validationErrors.Add("Keine Zahl hinterlegt");

            if (itg == 0)
                validationErrors.Add("Die Zahl darf nicht 0 sein");

            if (itg < 0)
                validationErrors.Add("Die Zahl zu niedrig");

            return validationErrors.Count == 0;
        }

        public bool ValidateIntegerAllow0(int? itg, out ICollection<string> validationErrors)
        {
            validationErrors = new List<String>();

            if (!itg.HasValue)
                validationErrors.Add("Keine Zahl hinterlegt");

            if (itg < 0)
                validationErrors.Add("Die Zahl zu niedrig");

            return validationErrors.Count == 0;
        }
    }
}
