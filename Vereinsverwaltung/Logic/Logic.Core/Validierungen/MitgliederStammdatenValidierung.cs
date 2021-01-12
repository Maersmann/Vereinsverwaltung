using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Logic.Core.Interfaces;

namespace Vereinsverwaltung.Logic.Core.Validierungen
{
    public class MitgliederStammdatenValidierung: IValidierung
    {
        public bool ValidateName(String inName, out ICollection<string> validationErrors)
        {
            validationErrors = new List<String>();

            if (inName.Length == 0)
                validationErrors.Add("Kein Name hinterlegt");

            return validationErrors.Count == 0;
        }

        public bool ValidateDatum(DateTime? inDate, out ICollection<string> validationErrors)
        {
            validationErrors = new List<String>();

            if (!inDate.HasValue)
                validationErrors.Add("Kein Datum hinterlegt");

            return validationErrors.Count == 0;
        }
    }
}
