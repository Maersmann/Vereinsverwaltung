using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Messages.BaseMessages;

namespace Vereinsverwaltung.Logic.Messages.MitgliederMessages
{
    public class OpenMitgliederStammdatenMessage : BaseStammdatenMessage
    {
        public int? MitgliedID { get; set; }
        
    }
}
