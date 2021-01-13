using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Types;

namespace Vereinsverwaltung.Logic.Messages.MitgliederMessages
{
    public class OpenMitgliederStammdatenMessage
    {
        public int? MitgliedID { get; set; }
        public State State { get; set; }
    }
}
